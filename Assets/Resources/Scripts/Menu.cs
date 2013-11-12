using UnityEngine;
using System.Collections;

public class Menu : MonoBehaviour {

	public Vector2 mousePosition;
	private RaycastHit raycastHit;	
	private GameObject hitObject;
	
	public bool currentMenu = false;
	public string backMenu = "StartMenu";
	
	void Update () 
	{
	
		if(Input.GetMouseButton(0) && currentMenu)
		{
			Vector3 origin = Camera.main.ScreenToWorldPoint(Input.mousePosition);

			Vector3 fwd = Vector3.forward;
			if(Physics.Raycast(origin, fwd, out raycastHit))
			{
				if(raycastHit.transform.gameObject.name == "Button")
				{
					hitObject = raycastHit.transform.gameObject;
					hitObject.GetComponent<Button>().onPress();
				}
				else
				{
					if(hitObject != null)
					{
						hitObject.GetComponent<Button>().onNotPressedNoMore();
						hitObject = null;
					}
				}
			}
		}
		
		// -----------------------------BackButton-----------------------------
		if(Application.platform == RuntimePlatform.Android || Application.platform == RuntimePlatform.IPhonePlayer)
		{
			if(currentMenu && Input.GetKey(KeyCode.Escape))
			{
				transform.parent.GetComponent<MenuTree>().currentMenu = backMenu;
			}
		}
	}
}
