using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class GAME_MANAGER : MonoBehaviour
{ 
    //MARIA T'ESTIMAM, PERD� PER� NO SABEM FER-HO AMB MENOS FUNCIONS SORRY :) (pov:un dissapte de sa nostra vida fent programaci�)

    public AudioClip[] songs;
    public int currentSong;
    private AudioSource _audioSource;
    public TextMeshProUGUI songText;
    public bool isPlaying;
    public float speed = 100;
    public disco discoScript;
    public Sprite[] images;
    public Image foto;
    public TextMeshProUGUI[] playlistText;
    public GameObject playlistPANEL;
    public bool hasClicked;
    public TextMeshProUGUI playlistTex;

    private void Awake()
    {
        _audioSource = GetComponent<AudioSource>();
        discoScript = FindObjectOfType<disco>();
    }

    private void Start()
    {
        currentSong = 0; //comen�a des de sa primera can��
        isPlaying = false;
        UpdateTextSong();
        UpdatePlaylistText();
        UpdateImage();

        //asegurar que no surti es panel des de es principi
        playlistPANEL.SetActive(false);
        hasClicked = false;
    }

    private void Update()
    {
        if(isPlaying == true) //mentre soni una can�o es disc se mou
        {
            discoScript.DiskMovement(); 
        }
    }

    public void PlaySong()//si posam m�s d'un par�metre no anir� be
    {
        _audioSource.clip = songs[currentSong]; //asignar es clip especific que volem (�Quina can�� volem reprodu�r?)
        _audioSource.Play();

        isPlaying = true;

        UpdateImage();
    }

    public void NextSong()
    {
        _audioSource.Stop(); //aturam can�� que sona

        //reiniciar s'array
        if (++currentSong >= songs.Length)
            {
            currentSong = 0;
            }

        UpdateTextSong(); //update text

        PlaySong();
    }

    public void AntSong()
    {
        _audioSource.Stop(); //aturam can�� que sona

        //reiniciar s'array

        if (--currentSong < 0)
        {
            currentSong = songs.Length-1;
        }
        UpdateTextSong();

        PlaySong();
    }

    public void RandomSong()
    {
        currentSong = Random.Range(0, songs.Length); //triam numero aleatori de can��
        PlaySong();
        UpdateTextSong();
    }

    private void UpdateTextSong()
    {
        songText.text = $"{songs[currentSong].name}";
    }

    public void RestartSong()
    {
        _audioSource.Stop(); //aturam sa can�� que est� sonant, perqu� reinicia
        PlaySong(); 
    }
 
    public void PauseSong() //pausa sa can�� i ja no est� playing res
    {
        _audioSource.Pause();
        isPlaying = false;
    }

    private void UpdateImage() //refresca sa imatge
    {
        foto.sprite = images[currentSong];
    }

    private void UpdatePlaylistText() //escrivim tots es nom de ses can�ons per ordre segons s'array de can�ons
    {
        for(int i=0; i< songs.Length ; i++)
        {
            playlistText[i].text = $"{songs[i].name}";
        }
    }
    
    public void PlaylistButtonPanel()
    {
        if (hasClicked == true) //si ja ha clickat un pic, que veu es panel + canvia es nom des bot� 
        {
            playlistPANEL.SetActive(false);
            playlistTex.text = $"playlist";
            hasClicked = false;

        }
        else //si no havia pitjat, (no veu es panel) el mostram i canviam es nom des bot� a back
        {
            playlistPANEL.SetActive(true);
            playlistTex.text = $"back";
            hasClicked = true;
        }
    }

    public void StopBTTN()
    {
        _audioSource.Stop(); //aturam musica del tot
        isPlaying = false;
    }

    public void PlaySong0(int songIdx)
    {
        //play audioclip 1
        _audioSource.clip = songs[songIdx]; 
        _audioSource.Play();

        currentSong = songIdx; //asignam a pi��n es valor de sa can��

        //refrescam imatge i text
        UpdateTextSong(); 
        UpdateImage();
    }
}


