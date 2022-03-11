// KeyBinding.cs
// Owned by Garabatos Inc.
// Created by: Andrea Navarro Zagarra (301185124)

using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class KeyBinding : MonoBehaviour
{
    public TMP_Text[] keys;
    public Button[] Btns;

    private bool check = false;



    public void SetJump()
    {
        StartCoroutine(SetKey(keys[0], Btns[0]));
        IEnumerator SetKey(TMP_Text x, Button y)
        {
            string z = x.text;
            x.text = "Hold key";
            y.GetComponent<Image>().color = new Color(0.16f, 0.67f, 0.87f, 0.7f);  //Blue for interface #29ABE2
            yield return new WaitForSeconds(0.8f);
            foreach(KeyCode kcode in System.Enum.GetValues(typeof(KeyCode)))
            {
                if (Input.GetKey(kcode))
                {
                    check = true;
                    Debug.Log("KeyCode down: " + kcode);
                    x.text = kcode.ToString().ToLower();
                    GameManager.Instance.GetComponent<InputManager>().SetJump(x.text);
                }
            }
            if(!check) {x.text = z;}
            y.GetComponent<Image>().color = new Color(75, 77, 77, 1);        //Gray for interface #4B4D4D
            check = false;
        }
    }

    public void SetRight()
    {
        StartCoroutine(SetKey(keys[1], Btns[1]));
        IEnumerator SetKey(TMP_Text x, Button y)
        {
            string z = x.text;
            x.text = "Hold key";
            y.GetComponent<Image>().color = new Color(0.16f, 0.67f, 0.87f, 0.7f);  //Blue for interface #29ABE2
            yield return new WaitForSeconds(0.8f);
            foreach(KeyCode kcode in System.Enum.GetValues(typeof(KeyCode)))
            {
                if (Input.GetKey(kcode))
                {
                    check = true;
                    Debug.Log("KeyCode down: " + kcode);
                    x.text = kcode.ToString().ToLower();
                    GameManager.Instance.GetComponent<InputManager>().SetRight(x.text);
                }
            }
            if(!check) {x.text = z;}
            y.GetComponent<Image>().color = new Color(75, 77, 77, 1);        //Gray for interface #4B4D4D
            check = false;
        }
    }

    public void SetLeft()
    {
        StartCoroutine(SetKey(keys[2], Btns[2]));
        IEnumerator SetKey(TMP_Text x, Button y)
        {
            string z = x.text;
            x.text = "Hold key";
            y.GetComponent<Image>().color = new Color(0.16f, 0.67f, 0.87f, 0.7f);  //Blue for interface #29ABE2
            yield return new WaitForSeconds(0.8f);
            foreach(KeyCode kcode in System.Enum.GetValues(typeof(KeyCode)))
            {
                if (Input.GetKey(kcode))
                {
                    check = true;
                    Debug.Log("KeyCode down: " + kcode);
                    x.text = kcode.ToString().ToLower();
                    GameManager.Instance.GetComponent<InputManager>().SetLeft(x.text);
                }
            }
            if(!check) {x.text = z;}
            y.GetComponent<Image>().color = new Color(75, 77, 77, 1);        //Gray for interface #4B4D4D
            check = false;
        }
    }

    public void SetForward()
    {
        StartCoroutine(SetKey(keys[3], Btns[3]));
        IEnumerator SetKey(TMP_Text x, Button y)
        {
            string z = x.text;
            x.text = "Hold key";
            y.GetComponent<Image>().color = new Color(0.16f, 0.67f, 0.87f, 0.7f);  //Blue for interface #29ABE2
            yield return new WaitForSeconds(0.8f);
            foreach(KeyCode kcode in System.Enum.GetValues(typeof(KeyCode)))
            {
                if (Input.GetKey(kcode))
                {
                    check = true;
                    Debug.Log("KeyCode down: " + kcode);
                    x.text = kcode.ToString().ToLower();
                    GameManager.Instance.GetComponent<InputManager>().SetForward(x.text);
                }
            }
            if(!check) {x.text = z;}
            y.GetComponent<Image>().color = new Color(75, 77, 77, 1);        //Gray for interface #4B4D4D
            check = false;
        }
    }

    public void SetBackward()
    {
        StartCoroutine(SetKey(keys[4], Btns[4]));
        IEnumerator SetKey(TMP_Text x, Button y)
        {
            string z = x.text;
            x.text = "Hold key";
            y.GetComponent<Image>().color = new Color(0.16f, 0.67f, 0.87f, 0.7f);  //Blue for interface #29ABE2
            yield return new WaitForSeconds(0.8f);
            foreach(KeyCode kcode in System.Enum.GetValues(typeof(KeyCode)))
            {
                if (Input.GetKey(kcode))
                {
                    check = true;
                    Debug.Log("KeyCode down: " + kcode);
                    x.text = kcode.ToString().ToLower();
                    GameManager.Instance.GetComponent<InputManager>().SetBackward(x.text);
                }
            }
            if(!check) {x.text = z;}
            y.GetComponent<Image>().color = new Color(75, 77, 77, 1);        //Gray for interface #4B4D4D
            check = false;
        }
    }

    public void SetShoot()
    {
        StartCoroutine(SetKey(keys[5], Btns[5]));
        IEnumerator SetKey(TMP_Text x, Button y)
        {
            string z = x.text;
            x.text = "Hold key";
            y.GetComponent<Image>().color = new Color(0.16f, 0.67f, 0.87f, 0.7f);  //Blue for interface #29ABE2
            yield return new WaitForSeconds(0.8f);
            foreach(KeyCode kcode in System.Enum.GetValues(typeof(KeyCode)))
            {
                if (Input.GetKey(kcode))
                {
                    check = true;
                    Debug.Log("KeyCode down: " + kcode);
                    x.text = kcode.ToString().ToLower();
                    GameManager.Instance.GetComponent<InputManager>().SetShoot(x.text);
                }
            }
            if(!check) {x.text = z;}
            y.GetComponent<Image>().color = new Color(75, 77, 77, 1);        //Gray for interface #4B4D4D
            check = false;
        }
    }

    public void SetSelect()
    {
        StartCoroutine(SetKey(keys[6], Btns[6]));
        IEnumerator SetKey(TMP_Text x, Button y)
        {
            string z = x.text;
            x.text = "Hold key";
            y.GetComponent<Image>().color = new Color(0.16f, 0.67f, 0.87f, 0.7f);  //Blue for interface #29ABE2
            yield return new WaitForSeconds(0.8f);
            foreach(KeyCode kcode in System.Enum.GetValues(typeof(KeyCode)))
            {
                if (Input.GetKey(kcode))
                {
                    check = true;
                    Debug.Log("KeyCode down: " + kcode);
                    x.text = kcode.ToString().ToLower();
                    GameManager.Instance.GetComponent<InputManager>().SetSelect(x.text);
                }
            }
            if(!check) {x.text = z;}
            y.GetComponent<Image>().color = new Color(75, 77, 77, 1);        //Gray for interface #4B4D4D
            check = false;
        }
    }

    public void SetSettings()
    {
        StartCoroutine(SetKey(keys[7], Btns[7]));
        IEnumerator SetKey(TMP_Text x, Button y)
        {
            string z = x.text;
            x.text = "Hold key";
            y.GetComponent<Image>().color = new Color(0.16f, 0.67f, 0.87f, 0.7f);  //Blue for interface #29ABE2
            yield return new WaitForSeconds(0.8f);
            foreach(KeyCode kcode in System.Enum.GetValues(typeof(KeyCode)))
            {
                if (Input.GetKey(kcode))
                {
                    check = true;
                    Debug.Log("KeyCode down: " + kcode);
                    x.text = kcode.ToString().ToLower();
                    GameManager.Instance.GetComponent<InputManager>().SetSettings(x.text);
                }
            }
            if(!check) {x.text = z;}
            y.GetComponent<Image>().color = new Color(75, 77, 77, 1);        //Gray for interface #4B4D4D
            check = false;
        }
    }

    public void SetPower1()
    {
        StartCoroutine(SetKey(keys[8], Btns[8]));
        IEnumerator SetKey(TMP_Text x, Button y)
        {
            string z = x.text;
            x.text = "Hold key";
            y.GetComponent<Image>().color = new Color(0.16f, 0.67f, 0.87f, 0.7f);  //Blue for interface #29ABE2
            yield return new WaitForSeconds(0.8f);
            foreach(KeyCode kcode in System.Enum.GetValues(typeof(KeyCode)))
            {
                if (Input.GetKey(kcode))
                {
                    check = true;
                    Debug.Log("KeyCode down: " + kcode);
                    x.text = kcode.ToString().ToLower();
                    GameManager.Instance.GetComponent<InputManager>().SetPower1(x.text);
                }
            }
            if(!check) {x.text = z;}
            y.GetComponent<Image>().color = new Color(75, 77, 77, 1);        //Gray for interface #4B4D4D
            check = false;
        }
    }

    public void SetPower2()
    {
        StartCoroutine(SetKey(keys[9], Btns[9]));
        IEnumerator SetKey(TMP_Text x, Button y)
        {
            string z = x.text;
            x.text = "Hold key";
            y.GetComponent<Image>().color = new Color(0.16f, 0.67f, 0.87f, 0.7f);  //Blue for interface #29ABE2
            yield return new WaitForSeconds(0.8f);
            foreach(KeyCode kcode in System.Enum.GetValues(typeof(KeyCode)))
            {
                if (Input.GetKey(kcode))
                {
                    check = true;
                    Debug.Log("KeyCode down: " + kcode);
                    x.text = kcode.ToString().ToLower();
                    GameManager.Instance.GetComponent<InputManager>().SetPower2(x.text);
                }
            }
            if(!check) {x.text = z;}
            y.GetComponent<Image>().color = new Color(75, 77, 77, 1);        //Gray for interface #4B4D4D
            check = false;
        }
    }

    public void SetPower3()
    {
        StartCoroutine(SetKey(keys[10], Btns[10]));
        IEnumerator SetKey(TMP_Text x, Button y)
        {
            string z = x.text;
            x.text = "Hold key";
            y.GetComponent<Image>().color = new Color(0.16f, 0.67f, 0.87f, 0.7f);  //Blue for interface #29ABE2
            yield return new WaitForSeconds(0.8f);
            foreach(KeyCode kcode in System.Enum.GetValues(typeof(KeyCode)))
            {
                if (Input.GetKey(kcode))
                {
                    check = true;
                    Debug.Log("KeyCode down: " + kcode);
                    x.text = kcode.ToString().ToLower();
                    GameManager.Instance.GetComponent<InputManager>().SetPower3(x.text);
                }
            }
            if(!check) {x.text = z;}
            y.GetComponent<Image>().color = new Color(75, 77, 77, 1);        //Gray for interface #4B4D4D
            check = false;
        }
    }

    public void SetPower4()
    {
        StartCoroutine(SetKey(keys[11], Btns[11]));
        IEnumerator SetKey(TMP_Text x, Button y)
        {
            string z = x.text;
            x.text = "Hold key";
            y.GetComponent<Image>().color = new Color(0.16f, 0.67f, 0.87f, 0.7f);  //Blue for interface #29ABE2
            yield return new WaitForSeconds(0.8f);
            foreach(KeyCode kcode in System.Enum.GetValues(typeof(KeyCode)))
            {
                if (Input.GetKey(kcode))
                {
                    check = true;
                    Debug.Log("KeyCode down: " + kcode);
                    x.text = kcode.ToString().ToLower();
                    GameManager.Instance.GetComponent<InputManager>().SetPower4(x.text);
                }
            }
            if(!check) {x.text = z;}
            y.GetComponent<Image>().color = new Color(75, 77, 77, 1);        //Gray for interface #4B4D4D
            check = false;
        }
    }

    /*IEnumerator SetKey(TMP_Text x, Button y)
    {
        string z = x.text;
        x.text = "Hold key";
        y.GetComponent<Image>().color = new Color(0.16f, 0.67f, 0.87f, 0.7f);  //Blue for interface #29ABE2
        yield return new WaitForSeconds(0.8f);
        foreach(KeyCode kcode in System.Enum.GetValues(typeof(KeyCode)))
        {
            if (Input.GetKey(kcode))
            {
                check = true;
                Debug.Log("KeyCode down: " + kcode);
                x.text = kcode.ToString().ToLower();
                GameManager.Instance.GetComponent<InputManager>().SetJump(x.text);
            }
        }
        if(!check) {x.text = z;}
        y.GetComponent<Image>().color = new Color(75, 77, 77, 1);        //Gray for interface #4B4D4D
    }*/

}
