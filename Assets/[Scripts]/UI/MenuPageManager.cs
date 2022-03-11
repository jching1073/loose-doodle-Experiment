// MenuPageManager.cs
// Owned by Garabatos Inc.
// Created by: Dohyun Kim (301058465)

using UnityEngine;
using UnityEngine.Events;

/// <summary>
///   <para>Manages the pages in a menu screen</para>
/// </summary>
[DisallowMultipleComponent]
public class MenuPageManager : MonoBehaviour
{
    /// <summary>
    ///   <para>A list of pages this component should manage</para>
    /// </summary>
    public GameObject[] pages;

    /// <summary>
    ///   <para>The page that should be active at the start</para>
    /// </summary>
    public GameObject mainPage;

    /// <summary>
    ///   <para>Event handlers for when a page is shown</para>
    /// </summary>
    public UnityEvent<GameObject> onPageShown;

    /// <summary>
    ///   <para>Enable the given page while disabling all other pages</para>
    /// </summary>
    /// <param name="page">The page to show</param>
    /// <param name="forceDeactivateOthers">Whether to call setActive(false) on everything else</param>
    public void ShowPage(GameObject page, bool forceDeactivateOthers = false)
    {
#if UNITY_EDITOR // check if the given page actually exists in the list
        if (System.Array.IndexOf(pages, page) < 0)
        {
            Debug.LogWarning($"mainPage `{mainPage.name}` is not in the list of pages - MenuPageManager `{name}`");
        }
#endif

        if (page.activeSelf && !forceDeactivateOthers) return;

        foreach (GameObject pageItem in pages)
        {
            pageItem.SetActive(false);
        }
        page.SetActive(true);
        onPageShown.Invoke(page);
    }

    /// <summary>
    ///   <para>Enable the page at the given index while disabling all other pages</para>
    /// </summary>
    /// <param name="pageIndex">The index of the page to show</param>
    public void ShowPageIndex(int pageIndex)
    {
        ShowPage(pages[pageIndex]);
    }

    /// <summary>
    ///   <para>Shows the main page</para>
    ///   <para>If the main page is not set, it shows the first page in the list.</para>
    /// </summary>
    public void ShowMainPage()
    {
        if (mainPage)
        {
            ShowPage(mainPage);
        }
        else
        {
            ShowPageIndex(0);
        }
    }

    /// <summary>
    ///   <para>
    ///     Toggle the given page, showing it if not already shown, and showing the main page if already shown
    ///   </para>
    /// </summary>
    /// <param name="page">The page to toggle</param>
    public void TogglePage(GameObject page)
    {
        if (page.activeSelf)
        {
            ShowMainPage();
        }
        else
        {
            ShowPage(page);
        }
    }

    /// <summary>
    ///   <para>
    ///     Toggle the page with the given index, showing it if not already shown, and showing the main page if already
    ///     shown
    ///   </para>
    /// </summary>
    /// <param name="pageIndex">The index of the page to toggle</param>
    public void TogglePageIndex(int pageIndex)
    {
        TogglePage(pages[pageIndex]);
    }

    void Awake()
    {
        if (mainPage)
        {
            ShowPage(mainPage, true);
        }
    }
}
