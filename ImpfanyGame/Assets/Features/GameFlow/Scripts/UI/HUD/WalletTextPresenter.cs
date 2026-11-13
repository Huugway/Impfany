using System.Collections;
using TMPro;
using UnityEngine;

public class WalletTextPresenter : NumericTextPresenter
{
    [SerializeField] private TMP_Text walletText;
    private PlayerWallet playerWallet;
    private Coroutine waitForWallet;

    protected override TMP_Text TargetText => walletText;
    protected override string Format(int number) => $"Coins: {number}";

    protected override void Subscribe()
    {
        playerWallet = FindFirstObjectByType<PlayerWallet>();
        if (playerWallet != null)
            BindWallet();
        else
            waitForWallet = StartCoroutine(BindWhenPlayerExists());
    }

    private IEnumerator BindWhenPlayerExists()
    {
        // GameFlow creates the Player after it activates the HUD.
        while (playerWallet == null)
        {
            yield return null;
            playerWallet = FindFirstObjectByType<PlayerWallet>();
        }

        BindWallet();
        waitForWallet = null;
    }

    private void BindWallet()
    {
        playerWallet.OnCoinsChanged += ShowNumber;
        playerWallet.PublishCoins();
    }

    protected override void Unsubscribe()
    {
        if (waitForWallet != null)
        {
            StopCoroutine(waitForWallet);
            waitForWallet = null;
        }

        if (playerWallet != null)
            playerWallet.OnCoinsChanged -= ShowNumber;
        playerWallet = null;
    }
}
