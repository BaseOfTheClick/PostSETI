var crosshairTexture : Texture2D;

function Start() {
    Screen.showCursor = false;
}

function OnGUI() {
    var mousePos : Vector3 = Input.mousePosition;
    var pos : Rect = Rect(mousePos.x-crosshairTexture.width*0.5,Screen.height - mousePos.y-crosshairTexture.height*0.5,crosshairTexture.width,crosshairTexture.height);
	GUI.DrawTexture( pos, crosshairTexture );
}
