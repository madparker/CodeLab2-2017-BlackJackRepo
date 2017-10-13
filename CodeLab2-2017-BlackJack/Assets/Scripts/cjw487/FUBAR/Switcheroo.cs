using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Switcheroo : MonoBehaviour
{
    public enum ShapeType
    {
        CIRCLE, TRIANGLE, DIAMOND, PENTAGON, HEXAGON
    }

    public bool changeThisRound;

    [SerializeField] private ShapeType _shape;
    public ShapeType Shape
    {
        get { return _shape; }
    }

    [SerializeField] private List<Sprite> _images;
    [SerializeField] protected Color _spriteColor;

    private Color selectedColor = new Color(0.734f, 0.305f, 0.145f);

    private ShuffleBag<Sprite> imageDeck;
    protected SpriteRenderer _renderer;

	// Use this for initialization
	void Start ()
    {
        changeThisRound = true;
        imageDeck = new ShuffleBag<Sprite>();
        _renderer = GetComponent<SpriteRenderer>();
        _spriteColor = _renderer.color;
        ChangeType(_renderer.sprite.name);
        for(int i = 0; i < _images.Count; i++)
        {
            imageDeck.Add(_images[i]);
        }

        Services.EventManager.Register<ChangeTokenEvent>(ChangeToken);
	}

    //  Changes the Shape enum to match the current shape
    protected void ChangeType(string spriteName)
    {
        switch (spriteName)
        {
            case "Circle":
                _shape = ShapeType.CIRCLE;
                break;
            case "Triangle":
                _shape = ShapeType.TRIANGLE;
                break;
            case "Diamond":
                _shape = ShapeType.DIAMOND;
                break;
            case "Pentagon":
                _shape = ShapeType.PENTAGON;
                break;
            case "Hexagon":
                _shape = ShapeType.HEXAGON;
                break;
            default:
                break;
        }

    }
    
    //  Changes the token image
    private void ChangeToken(ChangeTokenEvent e)
    {
        //  If we can change this round, do it
        if(changeThisRound)
            Next();
    }

    //  Shuffle Bar Stuff happens here
    public void Next()
    {
        //  The next thing is now our image
        _renderer.sprite = imageDeck.Next();
        //  Retain our previous color
        _renderer.color = _spriteColor;
        //  Change my enum
        ChangeType(_renderer.sprite.name);
    }

    protected virtual void Update()
    {
        //  If we are selected change the color
        if(changeThisRound)
        {
            _renderer.color = _spriteColor;
        }
        else
        {
            _renderer.color = selectedColor;
        }
    }

}
