using UnityEngine; 
using System.Collections; 
using System.Collections.Generic; 

public struct Button 
{ 
    public Rect rect; 
    public string url; 
    public string skin; 
     
    public Button(Rect rect, string url, string skin) 
    { 
        this.rect = rect; 
        this.url = url; 
        this.skin = skin; 
    } 
} 

public class Window : MonoBehaviour { 

    public Texture texture; 
    private Vector2 position; 
    public Vector2 size; 
    private Rect Rectangle; 
     
     
    public bool startActive; 
    private bool isActive; 
     
     
    public Vector2 buttonPosition; 
     
	public GUISkin skin;
     
    private List<Button> buttons; 

    void Start () { 
        isActive = startActive; 
		position = new Vector2(Screen.width*0.5f - size.x*0.5f,Screen.height*0.5f - size.y*0.5f);
		
		GUITexture logo = GameObject.Find("SynergyCiLogo").GetComponent<GUITexture>();
		logo.pixelInset = new Rect(Screen.width*0.5f -150, 830, logo.pixelInset.width, logo.pixelInset.height);
		
        buttons = new List<Button>(); 
		buttons.Add(new Button(new Rect(410,260,186,47),"http://www.status-c.pl","pageLink"));
		buttons.Add(new Button(new Rect(150,260,186,47),"http://www.status-c.pl/5/unity/assets/#scroll","assetLink"));
    } 
     
     
    void OnGUI() 
    { 
        if(isActive) 
        { 
			GUI.skin = skin;
			
            GUI.depth = -10;
            
			Rectangle = new Rect(position.x,position.y,size.x,size.y); 
            GUI.DrawTexture(Rectangle, texture); 
            bool close = GUI.Button(new Rect(position.x+buttonPosition.x,position.y+ buttonPosition.y, 31,33),"", "close"); 

            foreach(Button butt in buttons) 
                if(GUI.Button(new Rect(butt.rect.x+position.x, butt.rect.y+position.y, butt.rect.width, butt.rect.height), "", butt.skin))
                    Application.OpenURL(butt.url); 

            if(close) 
              	Hide();
        } 
		GUI.skin = skin;
		if(GUI.Button(new Rect(Screen.width -100, Screen.height - 100, 29, 47), "", "about"))
		{
			if(isActive) Hide(); else Show();
		}
    } 
     
	public void Show()
	{
		isActive = true;
	}
	
	public void Hide()
	{
		isActive = false;
	}
	
	public void Change()
	{
		isActive = !isActive;
	}
} 