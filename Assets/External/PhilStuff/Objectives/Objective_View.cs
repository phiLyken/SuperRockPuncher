using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System;

public class Objective_View : GenericView<Objective>
{

    public Text TitleTF;
    public Text DescrTF;
    public GameObject CompletedToggle;

    public event Action OnRemoved;

    
    protected override void OnSet(Objective item)
    {
        StartCoroutine(M_Extensions.YieldT((f) => { GetComponent<CanvasGroup>().alpha = f; }, 0.5f));

        m_Item.OnComplete += Updated;
        m_Item.OnCancel += Remove;
       // Debug.Log("Set Objective in View");
    }
    public override void Remove()
    {
        m_Item.OnCancel -= Remove;
        m_Item.OnComplete -= Updated;

        StartCoroutine(M_Extensions.YieldT((f) => { GetComponent<CanvasGroup>().alpha = 1-f; }, 0.5f));
        StartCoroutine(M_Math.ExecuteDelayed(0.5f,() => { OnRemoved.AttemptCall();  Destroy(this.gameObject); }));
     
    }
    
   

    public override void Updated()
    {
        TitleTF.text = m_Item.Config.Title;
        CompletedToggle.SetActive(m_Item.GetComplete());
    }

 
}

