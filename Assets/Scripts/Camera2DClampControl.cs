using UnityEngine;
using System.Collections;

public class Camera2DClampControl : MonoBehaviour {

    public Transform target;
    public SpriteRenderer background;
    Bounds bBounds;
    public Camera cam;
  

    void LateUpdate()
    {
        // la posicion del jugador.
        float xPosition = target.position.x;
        float yPosition = target.position.y;

        bBounds = background.bounds; // bound del escenario/fondo
        // Ahora vemos si xPosition hace que la camara se salga o no del fondo con un Mathf.Clamp
        // De valor minimo asignamos el extents.x del sprite del fondo (extents es solo la distancia desde el centro hasta el extremo, asi que tambien hay que indicar donde esta el centro del sprite)
        // + la distancia que cubre la camara desde el centro(cam.ortographicSize, que hay que multiplicarlo por cam.aspect para conseguir el valor x al parecer)

        if (bBounds.extents.x - 0.1f > cam.orthographicSize * cam.aspect)
        {
            xPosition = Mathf.Clamp(xPosition,
                bBounds.center.x - bBounds.extents.x + cam.orthographicSize * cam.aspect,
                bBounds.center.x + bBounds.extents.x - cam.orthographicSize * cam.aspect);
        }
        else { xPosition = bBounds.center.x; }
        if (bBounds.extents.y - 0.1f > cam.orthographicSize)
        {
            yPosition = Mathf.Clamp(yPosition,// lo mismo pero en la vertical, cam.orthographicSize es la distancia del centro de la camara hacia el extremo en la y, por lo que no hay que multiplicar por cam.aspect.
               bBounds.center.y - bBounds.extents.y + cam.orthographicSize,
               bBounds.center.y + bBounds.extents.y - cam.orthographicSize);
        }
        else { yPosition = bBounds.center.y; }

        // con el valor ya clampeado, ya podemos asignarlo a la posicion de la camara.
        transform.position = new Vector3(xPosition, yPosition, -10); // el -10 de la z para mantener la camara patras.

    }
}
