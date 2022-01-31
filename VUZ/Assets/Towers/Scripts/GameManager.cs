using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

namespace TowerGame
{
    public class GameManager : MonoBehaviour
    {
        [System.Serializable]
        public class ShopTower
        {
            public int Price = 100;
            public Tower Tower;
        }

        public static GameManager instance;
        public static List<Enemy> Enemies;

        public Canvas canvas;
        public Camera camera;
        public Transform TowersParent;
        public GameObject ShopUI;
        public GameObject LoseUI;
        public GameObject WinUI;

        public GameObject BonusMoneyPrefab;

        public Text MoneyText;
        public int Money = 200;

        public Text CastleHpUI;
        public Image CastleHpBarPivot;
        public float CastleDamageTimer = 0.5f;

        public Button PauseBtn, BoostBtn;

        float CastleHP = 100;
        ITowerPlace toPlace;

        List<Enemy> enemies = new List<Enemy>();
        float oldTime;
        bool isPlaying = true;

        public void Awake()
        {
            instance = this;
            Enemies = new List<Enemy>();
            UpdateMoneyText();
            CastleHpUI.text = Mathf.Round(CastleHP) + " / 100";
            Time.timeScale = 1;
        }

        public void CloseShop()
        {
            ShopUI.SetActive(false);
        }

        public void OpenShop(ITowerPlace t)
        {
            ShopUI.SetActive(true);
            toPlace = t;
        }

        public void SpawnTower(int i)
        {
            var tow = ShopManager.instance.TryBuy(i);
            if(tow != null)
            {
                Instantiate(tow.Tower, toPlace.GetTransform().localPosition, Quaternion.identity, GameManager.instance.TowersParent.transform);
                toPlace.Deactivate();
                CloseShop();
                UpdateMoneyText();
            }
        }



        public void AddBonusMoney(int m, Vector3 worldPos)
        {
            if(canvas && camera)
            {
                Money += m;
                UpdateMoneyText();
                var ui = Instantiate(BonusMoneyPrefab, canvas.transform);
                ui.transform.SetSiblingIndex(0);
                ui.GetComponent<RectTransform>().position = camera.WorldToScreenPoint(worldPos);
                ui.GetComponent<BonusMoneyPosition>().SetTargetPos(MoneyText.transform.position);
            }
        }

        bool paused;
        float oldTimeScale;
        public void PauseGame()
        {
            paused = !paused;

            var p = PauseBtn.colors;
            var b = BoostBtn.colors;

            if (paused)
            {
                oldTimeScale = Time.timeScale;
                Time.timeScale = 0;
                p = SetAllColors(p, Color.yellow);
            }
            else
            {
                Time.timeScale = oldTimeScale;
                p = SetAllColors(p, Color.white);
            }

            b = SetAllColors(b, Color.white);

            PauseBtn.colors = p;
            BoostBtn.colors = b;
        }

        bool time;
        public void ToggleBoost()
        {
            paused = false;
            time = !time;

            var p = PauseBtn.colors;
            var b = BoostBtn.colors;

            if (time)
            {
                Time.timeScale = 2;
                b = SetAllColors(b, Color.green);
            }
            else
            {
                Time.timeScale = 1;
                b = SetAllColors(b, Color.white);
            }

            p = SetAllColors(p, Color.white);

            PauseBtn.colors = p;
            BoostBtn.colors = b;
        }

        ColorBlock SetAllColors(ColorBlock block, Color col)
        {
            block.normalColor = col;
            block.highlightedColor = col;
            block.selectedColor = col;
            block.pressedColor = col;
            return block;
        }

        void UpdateMoneyText()
        {
            if(MoneyText)
                MoneyText.text = Money.ToString();
        }

        public void AddCastleEnemy(Enemy enemy)
        {
            enemies.Add(enemy);
        }

        public void GameWon()
        {
            if (CastleHP > 0 && isPlaying)
            {
                isPlaying = false;
                WinUI.SetActive(true);
                ShopUI.SetActive(false);
                LoseUI.SetActive(false);
            }
        }

        public void Restart()
        {
            var lev = SceneManager.GetActiveScene();
            SceneManager.LoadScene(lev.buildIndex);
        }

        public void NextLevel()
        {
            var lev = SceneManager.GetActiveScene().buildIndex;
            print(lev + " " + SceneManager.sceneCountInBuildSettings);
            if (lev+1 >= SceneManager.sceneCountInBuildSettings)
            {
                LoadMenu();
            }
            else
            {
                SceneManager.LoadScene(lev + 1);
            }
        }

        public void LoadMenu()
        {
            SceneManager.LoadScene(0);
        }

        void OnDestroy()
        {
            Time.timeScale = 1;
        }

        void Update()
        {
            if(enemies.Count > 0 && isPlaying)
            {
                if (Time.time - oldTime > CastleDamageTimer && CastleHP > 0)
                {
                    CastleHP = Mathf.Max(CastleHP - enemies.Count * 4, 0);
                    oldTime = Time.time;
                    for (int i = 0; i < enemies.Count; i++)
                    {
                        if (enemies[i] == null)
                        {
                            enemies.RemoveAt(i);
                            i--;
                        }
                        else
                        {
                            enemies[i].SetAttackMode();
                        }
                    }
                }
                CastleHpUI.text = Mathf.Round(CastleHP) + " / 100";
                //CastleHpBarPivot.transform.localScale = new Vector3(castleBarStartSize.x * (CastleHP / 100), castleBarStartSize.y, castleBarStartSize.z);
                CastleHpBarPivot.fillAmount = CastleHP / 100;
                if (CastleHP <= 0)
                {
                    GameLose();
                }
            }
        }

        void GameLose()
        {
            Time.timeScale = 0.01f;
            isPlaying = false;
            WinUI.SetActive(false);
            LoseUI.SetActive(true);
            CloseShop();
        }


    }
}
