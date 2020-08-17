using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterCustomiser : MonoBehaviour
{
    private bool m_topSelected;
    private bool m_bottomSelected;

    [HideInInspector]
    public SpriteRenderer m_head, m_hair, m_hat, m_face, m_mask, m_neck, m_body, m_pelvis,
                        m_handR, m_handL, m_armUpperR, m_armUpperL, m_armLowerR, m_armLowerL,
                        m_legUpperR, m_legUpperL, m_legLowerR, m_legLowerL, m_topBody,
                        m_topArmUpperR, m_topArmUpperL, m_topArmLowerR, m_topArmLowerL,
                        m_bottomPelvis, m_bottomLegUpperR, m_bottomLegUpperL,
                        m_bottomLegLowerR, m_bottomLegLowerL;

    public Sprite[] m_headOptions, m_hairOptions, m_hatOptions, m_faceOptions,
                    m_maskOptions, m_bodyOptions, m_pelvisOptions,
                    m_armRUpperOptions, m_armLUpperOptions, m_armRLowerOptions, m_armLLowerOptions,
                    m_legRUpperOptions, m_legLUpperOptions, m_legRLowerOptions, m_legLLowerOptions,
                    m_topChestOptions, m_topArmRUpperOptions, m_topArmLUpperOptions,
                    m_topArmRLowerOptions, m_topArmLLowerOptions, m_bottomPelvisOptions,
                    m_bottomLegUpperROptions, m_bottomLegUpperLOptions, m_bottomLegLowerROptions, m_bottomLegLowerLOptions;

    void Start(){
        InitCharComponents();
        InitCharChoices();
    }

    // get all the tagged character sprite components
    void InitCharComponents(){
        m_head = GameObject.FindGameObjectWithTag("CharHead").GetComponent<SpriteRenderer>();
        m_hair = GameObject.FindGameObjectWithTag("CharHair").GetComponent<SpriteRenderer>();
        m_face = GameObject.FindGameObjectWithTag("CharFace").GetComponent<SpriteRenderer>();
        m_neck = GameObject.FindGameObjectWithTag("CharNeck").GetComponent<SpriteRenderer>();
        m_body = GameObject.FindGameObjectWithTag("CharBody").GetComponent<SpriteRenderer>();

        m_pelvis = GameObject.FindGameObjectWithTag("CharPelvis").GetComponent<SpriteRenderer>();
        m_hat = GameObject.FindGameObjectWithTag("CharHat").GetComponent<SpriteRenderer>();
        m_mask = GameObject.FindGameObjectWithTag("CharMask").GetComponent<SpriteRenderer>();

        m_handR = GameObject.FindGameObjectWithTag("CharHandR").GetComponent<SpriteRenderer>();
        m_handL = GameObject.FindGameObjectWithTag("CharHandL").GetComponent<SpriteRenderer>();

        m_armUpperR = GameObject.FindGameObjectWithTag("CharArmUR").GetComponent<SpriteRenderer>();
        m_armUpperL = GameObject.FindGameObjectWithTag("CharArmUL").GetComponent<SpriteRenderer>();
        m_armLowerR = GameObject.FindGameObjectWithTag("CharArmLR").GetComponent<SpriteRenderer>();
        m_armLowerL = GameObject.FindGameObjectWithTag("CharArmLL").GetComponent<SpriteRenderer>();
        
        m_legUpperR = GameObject.FindGameObjectWithTag("CharLegUR").GetComponent<SpriteRenderer>();
        m_legUpperL = GameObject.FindGameObjectWithTag("CharLegUL").GetComponent<SpriteRenderer>();
        m_legLowerR = GameObject.FindGameObjectWithTag("CharLegLR").GetComponent<SpriteRenderer>();
        m_legLowerL = GameObject.FindGameObjectWithTag("CharLegLL").GetComponent<SpriteRenderer>();

        m_topBody = GameObject.FindGameObjectWithTag("CharTopChest").GetComponent<SpriteRenderer>();
        m_topArmUpperR = GameObject.FindGameObjectWithTag("CharTopAUR").GetComponent<SpriteRenderer>();
        m_topArmUpperL = GameObject.FindGameObjectWithTag("CharTopAUL").GetComponent<SpriteRenderer>();
        m_topArmLowerR = GameObject.FindGameObjectWithTag("CharTopALR").GetComponent<SpriteRenderer>();
        m_topArmLowerL = GameObject.FindGameObjectWithTag("CharTopALL").GetComponent<SpriteRenderer>();

        m_bottomPelvis = GameObject.FindGameObjectWithTag("CharBottomWaist").GetComponent<SpriteRenderer>();
        m_bottomLegUpperR = GameObject.FindGameObjectWithTag("CharBottomLUR").GetComponent<SpriteRenderer>();
        m_bottomLegUpperL = GameObject.FindGameObjectWithTag("CharBottomLUL").GetComponent<SpriteRenderer>();
        m_bottomLegLowerR = GameObject.FindGameObjectWithTag("CharBottomLLR").GetComponent<SpriteRenderer>();
        m_bottomLegLowerL = GameObject.FindGameObjectWithTag("CharBottomLLL").GetComponent<SpriteRenderer>();
    }

    // load the character customisation choices if it exists
    void InitCharChoices(){
        ChangeCharSkinColor(GameManager.Instance.GetSelectedColor());
        ChangeCharHairColor(GameManager.Instance.GetSelectedHairColor());

        m_topSelected = true;
        m_bottomSelected = false;
        ChangeCharClothesColor(GameManager.Instance.GetSelectedTopColor());
        Debug.Log("top color: " + GameManager.Instance.GetSelectedTopColor());
        m_topSelected = false;
        m_bottomSelected = true;
        ChangeCharClothesColor(GameManager.Instance.GetSelectedBottomColor());
        Debug.Log("bottom color: " + GameManager.Instance.GetSelectedBottomColor());
    
        ChangeCharHeadChoice(GameManager.Instance.GetSelectedHead());
        ChangeCharHairChoice(GameManager.Instance.GetSelectedHair());
        ChangeCharHatChoice(GameManager.Instance.GetSelectedHat());
        ChangeCharFaceChoice(GameManager.Instance.GetSelectedFace());
        ChangeCharMaskChoice(GameManager.Instance.GetSelectedMask());
        ChangeCharTopChoice(GameManager.Instance.GetSelectedTop());
        ChangeCharBottomChoice(GameManager.Instance.GetSelectedBottom());

        m_topSelected = m_bottomSelected = false;
    }

#region Character Customisation Changer Methods
    void ChangeCharSkinColor(int count){
        m_head.color = GameManager.Instance.k_SkinTones[count];
        //charFace.color = GameManager.Instance.k_SkinTones[count];
        m_body.color = GameManager.Instance.k_SkinTones[count];
        m_pelvis.color = GameManager.Instance.k_SkinTones[count];
        m_neck.color = GameManager.Instance.k_SkinTones[count];
        m_handR.color = GameManager.Instance.k_SkinTones[count];
        m_handL.color = GameManager.Instance.k_SkinTones[count];
        m_armUpperR.color = GameManager.Instance.k_SkinTones[count];
        m_armUpperL.color = GameManager.Instance.k_SkinTones[count];
        m_armLowerR.color = GameManager.Instance.k_SkinTones[count];
        m_armLowerL.color = GameManager.Instance.k_SkinTones[count];
        m_legUpperR.color = GameManager.Instance.k_SkinTones[count];
        m_legUpperL.color = GameManager.Instance.k_SkinTones[count];
        m_legLowerR.color = GameManager.Instance.k_SkinTones[count];
        m_legLowerL.color = GameManager.Instance.k_SkinTones[count];

        GameManager.Instance.SetSelectedColor(count);
    }

    void ChangeCharHairColor(int count){
        m_hair.color = GameManager.Instance.k_HairColors[count];

        GameManager.Instance.SetSelectedHairColor(count);
    }

    void ChangeCharClothesColor(int count){
        if(m_topSelected){
            m_topBody.color = GameManager.Instance.k_ClothesColors[count];
            m_topArmLowerR.color = GameManager.Instance.k_ClothesColors[count];
            m_topArmLowerL.color = GameManager.Instance.k_ClothesColors[count];
            m_topArmUpperR.color = GameManager.Instance.k_ClothesColors[count];
            m_topArmUpperL.color = GameManager.Instance.k_ClothesColors[count];

            GameManager.Instance.SetSelectedTopColor(count);
        }

        if(m_bottomSelected){
            m_bottomPelvis.color = GameManager.Instance.k_ClothesColors[count];
            m_bottomLegLowerR.color = GameManager.Instance.k_ClothesColors[count];
            m_bottomLegLowerL.color = GameManager.Instance.k_ClothesColors[count];
            m_bottomLegUpperR.color = GameManager.Instance.k_ClothesColors[count];
            m_bottomLegUpperL.color = GameManager.Instance.k_ClothesColors[count];

            GameManager.Instance.SetSelectedBottomColor(count);
        }
    }

    void ChangeCharHeadChoice(int count){
        m_head.sprite = m_headOptions[count];

        GameManager.Instance.SetSelectedHead(count);
    }

    void ChangeCharHairChoice(int count){
        m_hair.sprite = m_hairOptions[count];

        GameManager.Instance.SetSelectedHair(count);
    }

    void ChangeCharHatChoice(int count){
        m_hat.sprite = m_hatOptions[count];

        GameManager.Instance.SetSelectedHat(count);
    }

    void ChangeCharFaceChoice(int count){
        m_face.sprite = m_faceOptions[count];

        GameManager.Instance.SetSelectedFace(count);
    }

    void ChangeCharMaskChoice(int count){
        m_mask.sprite = m_maskOptions[count];

        GameManager.Instance.SetSelectedMask(count);
    }

    void ChangeCharTopChoice(int count){
        m_topBody.sprite = m_topChestOptions[count];
        m_topArmLowerR.sprite = m_topArmRLowerOptions[count];
        m_topArmLowerL.sprite = m_topArmLLowerOptions[count];
        m_topArmUpperR.sprite = m_topArmRUpperOptions[count];
        m_topArmUpperL.sprite = m_topArmLUpperOptions[count];

        GameManager.Instance.SetSelectedTop(count);
    }

    void ChangeCharBottomChoice(int count){
        m_bottomPelvis.sprite = m_bottomPelvisOptions[count];
        m_bottomLegLowerR.sprite = m_bottomLegLowerROptions[count];
        m_bottomLegLowerL.sprite = m_bottomLegLowerLOptions[count];
        m_bottomLegUpperR.sprite = m_bottomLegUpperROptions[count];
        m_bottomLegUpperL.sprite = m_bottomLegUpperLOptions[count];

        GameManager.Instance.SetSelectedBottom(count);
    }
#endregion

#region Character Customisation Call Methods
    public void ChangeSkinColor(int skinInt){
        ChangeCharSkinColor(skinInt);
    }

    public void ChangeHairColor(int hairColorInt){
        ChangeCharHairColor(hairColorInt);
    }

    public void ChangeClothesColor(int clothesColorInt){
        ChangeCharClothesColor(clothesColorInt);
    }

    public void ChangeHead(int headInt){
        ChangeCharHeadChoice(headInt);
    }

    public void ChangeHair(int hairInt){
        ChangeCharHairChoice(hairInt);
    }

    public void ChangeHat(int hatInt){
        ChangeCharHatChoice(hatInt);
    }

    public void ChangeFace(int faceInt){
        ChangeCharFaceChoice(faceInt);
    }
    
    public void ChangeMask(int maskInt){
        ChangeCharMaskChoice(maskInt);
    }

    public void ChangeTop(int topInt){
        m_topSelected = true;
        m_bottomSelected = false;

        ChangeCharTopChoice(topInt);
    }

    public void ChangeBottom(int bottomInt){
        m_topSelected = false;
        m_bottomSelected = true;

        ChangeCharBottomChoice(bottomInt);
    }
#endregion
}
