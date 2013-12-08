using UnityEngine;
using System.Collections;

public class AnimationScriptCan : MonoBehaviour {

	public int uvTieX = 1;
	public int uvTieY = 1;
	public int fps = 10; 
	public bool isLeft = false;
	public int row = 0;
	public int numberOfFrames = 15;
	public bool loop = true;
	public bool backwards = false;
    public bool endOfAnimation = false;
	public int index = 0;
	public float delay = 0;
	
	private Vector2 size;
	private Renderer myRenderer;
	private int lastIndex = -1;
	private int lastRow = -1;
	private float TimeForNextIndex = 0;
	 
	void Start () 
	{
		myRenderer = renderer;
		if(myRenderer == null)
			enabled = false;
	}
	// Update is called once per frame
	void Update()
	{
		//if(delay > 0)
		//{
		//	delay -= Time.deltaTime;
		//	
		//	myRenderer.material.color = new Color(1f, 1f, 1f, 0f);
		//}
		//else
		//	myRenderer.material.color = new Color(1f, 1f, 1f, 1f);
		
		
		
		
		
        size = new Vector2((1.0f / uvTieX), 1.0f / uvTieY);

		// Calculate index
		if(Time.timeSinceLevelLoad > TimeForNextIndex && fps > 0)
		{
			TimeForNextIndex = Time.timeSinceLevelLoad + (float)(1f / fps);
			index++;	
		}
		
		if(index > numberOfFrames - 1 && fps != 0)
		{
			if(loop)
				index = 0;
			else
			{
				index = numberOfFrames - 1;
				endOfAnimation = true;
			}
		}
		
    	if(index != lastIndex || row != lastRow)
		{
			// plus one if left
			int uIndex = index + (isLeft ? 1 : 0) + (isLeft && backwards ? -2 : 0);
      
			// build offset
			// v coordinate is the bottom of the image in opengl so we need to invert.
            Vector2 offset;
			offset.x = (backwards ? (numberOfFrames - 1 - uIndex) * size.x : uIndex * size.x);
            offset.y = 1.0f - size.y - row * size.y;

            myRenderer.material.mainTextureOffset = offset;
			size.x *= (isLeft ? -1 : 1);
            myRenderer.material.mainTextureScale = size;
           
			lastIndex = index;
			lastRow = row;
			
			endOfAnimation = (index == numberOfFrames -1);
		}
	}
	
	// changeAnimation
	public void setAnimation(int newRow, int newNumberOfFrames = 15, bool newLoop = true, int newFps = 16, bool newBackwards = false, int newUvTieX = 16)
	{
		row = newRow;
		numberOfFrames = newNumberOfFrames;
		loop = newLoop;
		endOfAnimation = false;
		fps = newFps;
		backwards = newBackwards;
		index = 0;
		uvTieX = newUvTieX;
	}

}
