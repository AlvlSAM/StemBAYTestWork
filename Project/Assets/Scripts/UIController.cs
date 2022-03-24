using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIController : MonoBehaviour
{

    private int selectedBomb = 0;
    private CharacterController characterController;
    public List<int> nowBombs;

    [SerializeField]
    private GameConfiguration gc;

    [SerializeField]
    private Image BombImg;

    [SerializeField]
    private TMP_Text countBombs;

    [SerializeField]
    private List<Sprite> bombImgs;


    // Start is called before the first frame update
    void Start()
    {
        Application.targetFrameRate = 144;
        int[] buf = new int[gc.CountBombs.Count];
        gc.CountBombs.CopyTo(buf);
        nowBombs = new List<int>(buf);
        characterController = FindObjectOfType<CharacterController>();

        BombImg.sprite = bombImgs[selectedBomb];
        countBombs.text = nowBombs[selectedBomb].ToString();
    }

    public void MoveUp()
    {
        characterController.MoveUp();
    }
    public void MoveRight()
    {
        characterController.MoveRight();
    }
    public void MoveDown()
    {
        characterController.MoveDown();
    }
    public void MoveLeft()
    {
        characterController.MoveLeft();
    }
    public void StopMove()
    {
        characterController.StopMove();
    }
    public void ChangeBomb(int i)
    {
        if (selectedBomb + i > nowBombs.Count - 1)
        {
            selectedBomb = 0;
        }
        else if (selectedBomb + i < 0)
        {
            selectedBomb = nowBombs.Count - 1;
        }
        else
        {
            selectedBomb += i;
        }

        BombImg.sprite = bombImgs[selectedBomb];
        countBombs.text = nowBombs[selectedBomb].ToString();

        characterController.ChangeBomb(selectedBomb);
    }
    public void PlantBomb()
    {
        if(characterController.PlantBomb())
        {
            nowBombs[selectedBomb] -= 1;
            countBombs.text = nowBombs[selectedBomb].ToString();
        }
    }

}
