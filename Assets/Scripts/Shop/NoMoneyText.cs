using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NoMoneyText : MonoBehaviour
{
    private void OnEnable() {
        StartCoroutine(CountForDisappearing());
    }

    IEnumerator CountForDisappearing(){
        yield return new WaitForSeconds(2f);
        gameObject.SetActive(false);
    }
}
