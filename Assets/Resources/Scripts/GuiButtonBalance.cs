using UnityEngine;
using System.Collections;
using System.Reflection;

public class GuiButtonBalance : MonoBehaviour
{
	public Texture buttonTexture;
	
	public bool showBalance = false;
	
	void OnGUI ()
	{
		if(GUI.Button(new Rect(10, 10, 100, 70), "Balance"))
			showBalance = !showBalance;
		
		if(showBalance)
		{
			int buttonY = 120;
			Component script = GetComponent<Player>();
			int i = 0;
			foreach(FieldInfo fi in script.GetType().GetFields())
			{
				
				float addifier;
				switch(i)
				{
					case 0:
					case 1:
					case 2:
					case 3:
						addifier = 0.1f;
						break;
					case 4:
					case 5:
						addifier = 0.1f;
						break;
					case 6:
					case 7:
					case 8:
						addifier = 0.01f;
						break;
					default:
						return;
				}
				
				System.Object obj = (System.Object)script;
				string name = fi.Name;
				System.Object val = fi.GetValue(obj);
				
				GUI.Box(new Rect(10, buttonY, 200, 70), name + ": " + val.ToString());
				
				if(GUI.Button(new Rect(250, buttonY, 100, 70), "+"))
				{
					float fVal = (float)val;
					fVal += addifier;
					fi.SetValue(obj, (System.Object)fVal);
				}
				
				if(GUI.Button(new Rect(400, buttonY, 100, 70), "-"))
				{
					float fVal = (float)val;
					fVal -= addifier;
					fi.SetValue(obj, (System.Object)fVal);
				}
				
				buttonY += 120;
				++i;
			}
		}
	}
}
