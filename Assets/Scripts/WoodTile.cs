using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class WoodTile : Tile
{
    private bool isUpLeftFull;
    private bool isUpRightFull;
    private bool isDownLeftFull;
    private bool isDownRightFull;

    [Space(10)]

    [SerializeField]
    protected Sprite NoBlank;

    [SerializeField]
    protected Sprite RightBlank;

    [SerializeField]
    protected Sprite LeftBlank;

    [SerializeField]
    protected Sprite LeftRightBlank;

    [SerializeField]
    protected Sprite UpBlank;

    [SerializeField]
    protected Sprite UpRightBlank;

    [SerializeField]
    protected Sprite UpLeftRightBlank;

    [SerializeField]
    protected Sprite UpLeftBlank;

    [SerializeField]
    protected Sprite UpDownRightBlank;

    [SerializeField]
    protected Sprite UpDownLeftBlank;

    [SerializeField]
    protected Sprite UpDownBlank;

    [SerializeField]
    protected Sprite AllBlank;

    [SerializeField]
    protected Sprite DownRightBlank;

    [SerializeField]
    protected Sprite DownLeftBlank;

    [SerializeField]
    protected Sprite DownLeftRightBlank;

    [SerializeField]
    protected Sprite DownBlank;

    // Corners Start from Below:

    [SerializeField]
    protected Sprite UpLeftCorner;

    [SerializeField]
    protected Sprite UpRightCorner;

    [SerializeField]
    protected Sprite UpFullDownLeftCorner;

    [SerializeField]
    protected Sprite UpFullDownRightCorner;

    [SerializeField]
    protected Sprite UpCorners;

    [SerializeField]
    protected Sprite LeftCorners;

    [SerializeField]
    protected Sprite DownLeftCorner;

    [SerializeField]
    protected Sprite DownRightCorner;

    [SerializeField]
    protected Sprite DownFullUpLeftCorner;

    [SerializeField]
    protected Sprite DownFullUpRightCorner;

    [SerializeField]
    protected Sprite DownCorners;

    [SerializeField]
    protected Sprite RightCorners;

    [SerializeField]
    protected Sprite UpLeftFullDownRightCorner;

    [SerializeField]
    protected Sprite UpFullDownCorners;

    [SerializeField]
    protected Sprite UpRightFullDownLeftCorner;

    [SerializeField]
    protected Sprite LeftFullRightCorners;

    [SerializeField]
    protected Sprite AllCorners;

    [SerializeField]
    protected Sprite RightFullLeftCorners;

    [SerializeField]
    protected Sprite DownLeftFullUpRightCorner;

    [SerializeField]
    protected Sprite DownFullUpCorners;

    [SerializeField]
    protected Sprite DownRightFullUpLeftCorner;

    [SerializeField]
    protected Sprite UpLeftDownRightCorners;

    [SerializeField]
    protected Sprite UpRightDownLeftCorners;

    [SerializeField]
    protected Sprite LeftFullDownRightCorner;

    [SerializeField]
    protected Sprite RightFullDownLeftCorner;

    [SerializeField]
    protected Sprite ExceptDownRightCorners;

    [SerializeField]
    protected Sprite ExceptDownLeftCorners;

    [SerializeField]
    protected Sprite LeftFullUpRightCorner;

    [SerializeField]
    protected Sprite RightFullUpLeftCorner;

    [SerializeField]
    protected Sprite ExceptUpRightCorners;

    [SerializeField]
    protected Sprite ExceptUpLeftCorners;

    private void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();

        // Checking Each Side
        GetComponent<Tile>().checkTileBoundaries();

        bool areFourSidesFull = isUpFull || isLeftFull || isRightFull || isDownFull;
        bool areFourCornersFull = isUpLeftFull || isUpRightFull || isDownLeftFull || isDownRightFull;
        if (areFourSidesFull)
        {
            if (isUpFull)
            {
                if (isDownFull)
                {
                    if (isRightFull && isLeftFull)
                    {
                        // 이때 변은 없지만 모서리는 있을 수 있기에
                        // 여러 가지 경우의 수가 존재한다.
                        checkOnlyForCorners(areFourCornersFull);
                    }
                    else if (isRightFull)
                    {
                        if (isDownRightFull && isUpRightFull)
                        {
                            spriteRenderer.sprite = UpDownRightBlank;
                        }
                        else if (isDownRightFull)
                        {
                            spriteRenderer.sprite = LeftFullUpRightCorner;
                        }
                        else if (isUpRightFull)
                        {
                            spriteRenderer.sprite = LeftFullDownRightCorner;
                        }
                        else spriteRenderer.sprite = LeftFullRightCorners;
                    }
                    else if (isLeftFull)
                    {
                        if (isUpLeftFull && isDownLeftFull)
                        {
                            spriteRenderer.sprite = UpDownLeftBlank;
                        }
                        else if (isUpLeftFull)
                        {
                            spriteRenderer.sprite = RightFullDownLeftCorner;
                        }
                        else if (isDownLeftFull)
                        {
                            spriteRenderer.sprite = RightFullUpLeftCorner;
                        }
                        else spriteRenderer.sprite = RightFullLeftCorners;
                    }
                    else
                    {
                        spriteRenderer.sprite = UpDownBlank;
                    }
                }
                else
                {
                    if (isRightFull && isLeftFull)
                    {
                        if (isUpLeftFull && isUpRightFull)
                        {
                            spriteRenderer.sprite = UpLeftRightBlank; 
                        }
                        else if (isUpLeftFull)
                        {
                            spriteRenderer.sprite = DownFullUpRightCorner;
                        }
                        else if (isUpRightFull)
                        {
                            spriteRenderer.sprite = DownFullUpLeftCorner;
                        }
                        else
                            spriteRenderer.sprite = DownFullUpCorners;
                    }
                    else if (isLeftFull)
                    {
                        if (!isUpLeftFull)
                        {
                            spriteRenderer.sprite = DownRightFullUpLeftCorner;
                        }
                        else spriteRenderer.sprite = UpLeftBlank;
                    }
                    else if (isRightFull)
                    {
                        if (!isUpRightFull)
                        {
                            spriteRenderer.sprite = DownLeftFullUpRightCorner;
                        }
                        else spriteRenderer.sprite = UpRightBlank;
                    }
                    else
                    {
                        spriteRenderer.sprite = UpBlank;
                    }
                }
            }
            else if (isLeftFull)
            {
                if (isRightFull)
                {
                    if (isDownFull)
                    {
                        if (isDownLeftFull && isDownRightFull)
                        {
                            spriteRenderer.sprite = DownLeftRightBlank; 
                        }
                        else if (isDownLeftFull)
                        {
                            spriteRenderer.sprite = UpFullDownRightCorner;
                        }
                        else if (!isDownRightFull)
                        {
                            spriteRenderer.sprite = UpFullDownLeftCorner;
                        }
                        else
                            spriteRenderer.sprite = UpFullDownCorners;
                    }
                    else
                    {
                        spriteRenderer.sprite = LeftRightBlank;
                    }
                }
                else
                {
                    if (isDownFull)
                    {
                        if (!isDownLeftFull)
                        {
                            spriteRenderer.sprite = UpRightFullDownLeftCorner;
                        }
                        else spriteRenderer.sprite = DownLeftBlank;
                    }
                    else
                    {
                        spriteRenderer.sprite = LeftBlank;
                    }
                }
            }
            else if (isRightFull)
            {
                if (isDownFull)
                {
                    if (!isDownRightFull)
                    {
                        spriteRenderer.sprite = UpLeftFullDownRightCorner;
                    }
                    else spriteRenderer.sprite = DownRightBlank;
                }
                else
                {
                    spriteRenderer.sprite = RightBlank;
                }
            }
            else if (isDownFull)
            {
                spriteRenderer.sprite = DownBlank;
            }
        }
        else
        {
            spriteRenderer.sprite = NoBlank;
        }
    }

    private void checkOnlyForCorners(bool areFourCornersFull)
    {
        if (areFourCornersFull)
        {
            if (isUpLeftFull)
            {
                if (isDownLeftFull)
                {
                    if (isUpRightFull && isDownRightFull)
                    {
                        spriteRenderer.sprite = AllBlank;
                    }
                    else if (isUpRightFull)
                    {
                        spriteRenderer.sprite = DownRightCorner;
                    }
                    else if (isDownRightFull)
                    {
                        spriteRenderer.sprite = UpRightCorner;
                    }
                    else spriteRenderer.sprite = RightCorners;
                }
                else
                {
                    if (isUpRightFull && isDownRightFull)
                    {
                        spriteRenderer.sprite = DownLeftCorner;
                    }
                    else if (isUpRightFull)
                    {
                        spriteRenderer.sprite = DownCorners;
                    }
                    else if (isDownRightFull)
                    {
                        spriteRenderer.sprite = UpRightDownLeftCorners;
                    }
                    else spriteRenderer.sprite = ExceptUpLeftCorners;
                }
            }
            // UpLeftCorner 필수
            else if (isUpRightFull)
            {
                if (isDownRightFull && isDownLeftFull)
                {
                    spriteRenderer.sprite = UpLeftCorner;
                }
                else if (isDownRightFull)
                {
                    spriteRenderer.sprite = LeftCorners;
                }
                else if (isDownLeftFull)
                {
                    spriteRenderer.sprite = UpLeftDownRightCorners;
                }
                else spriteRenderer.sprite = ExceptUpRightCorners;
            }
            else if (isDownLeftFull)
            {
                if (isDownRightFull)
                {
                    spriteRenderer.sprite = UpCorners;
                }
                else spriteRenderer.sprite = ExceptDownLeftCorners;
            }
            else if (isDownRightFull)
            {
                spriteRenderer.sprite = ExceptDownRightCorners;
            }
        }
        else
        {
            spriteRenderer.sprite = AllCorners;
        }
    }

    // WoodTile은 대각선 위치도 확인해야 함
    public override void checkTileBoundaries()
    {
        base.checkTileBoundaries();
        isUpLeftFull = Physics2D.OverlapCircle(transform.position + Vector3.left + Vector3.up, 0.1f, tileLayerMask);
        isUpRightFull = Physics2D.OverlapCircle(transform.position + Vector3.right + Vector3.up, 0.1f, tileLayerMask);
        isDownLeftFull = Physics2D.OverlapCircle(transform.position + Vector3.left + Vector3.down, 0.1f, tileLayerMask);
        isDownRightFull = Physics2D.OverlapCircle(transform.position + Vector3.right + Vector3.down, 0.1f, tileLayerMask);
    }
}