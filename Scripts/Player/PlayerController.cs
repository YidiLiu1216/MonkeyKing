using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    // Start is called before the first frame update
    public bool isGrounded = true;
    
    //public Transform grab;
    [Header("Basic Value")]
    [SerializeField] PlayerData playerData;
    [SerializeField] Transform levelPoint;
    public float maxHeight = 0;
    [Header("AnimeSet")]
    [SerializeField] Animator animator;
    [SerializeField] SpriteRenderer sprite;
    int currentHealth;
    Vector3 lastStandPosition = Vector3.zero;
    [Header("DamageControl")]
    public Camera MainCamera;
    [SerializeField] float damageZoneOffSet;
    [SerializeField] float maxInvincibleTime;
    [SerializeField] float rebornTime;
    [SerializeField] AudioSource fallAudio;
    [SerializeField] AudioSource damageAuido;
    float InvincibleTimer;
    bool isInvincible = false;
    Vector2 damageZoneSize;
    [SerializeField] int maxHealth = 3;
    [Header("JumpControl")]
    [SerializeField] int jumpAngleRange;
    [SerializeField] float jumpAngleChangeSpeed;
    [SerializeField] float jumpForce;
    [SerializeField] GameObject topPointer;
    [SerializeField] GameObject jumpArc;
    [SerializeField] AudioSource JumpAudio;
    float jumpAngle=0;
    int jumpAngleChangeDirection=-1;
    [Header("SwingControl")]
    public bool isSwinging = false;
    [SerializeField] Transform playerParents;
    [SerializeField] float maxSwingColdDownTime;
    [SerializeField] float swingForce;
    [SerializeField] AudioSource SwingAudio;
    GrabVine grabVine;
    float swingColdeDownTimer;
    bool useSwingTimer = false;
    [Header("AimControl")]
    [SerializeField] int aimAngleRange;
    [SerializeField] float aimAngleChangeSpeed;
    [SerializeField] float aimForce;
    [SerializeField] GameObject aimPointer;
    [SerializeField] GameObject aimArc;
    [SerializeField] AudioSource aimAudio;
    int aimAngleChangeDirection = -1;
    bool isAimming = false;
    float throwAngle=0;
    [Header("ThrowControl")]
    int throwObject;
    [SerializeField] List<GameObject> fruitPrefabs=new List<GameObject>();
    [SerializeField] List<int> fruitHold = new List<int>();
    [SerializeField] List<FruitData> fruitdatas = new List<FruitData>();
    [SerializeField] int cannotTrowFruitNum;
    [SerializeField] AudioSource ThrowAudio;
    [Header("OtherAudio")]
    [SerializeField] AudioSource PickUpFruitAudio;
    [SerializeField] AudioSource AddHealthAudio;
    Rigidbody2D rigidbody2d;
    BoxCollider2D boxCollider2d;
    Quaternion initRotationTop;
    Quaternion initRotationAim;
    bool faceleft=false;
    private void Awake()
    {
        rigidbody2d = gameObject.GetComponent<Rigidbody2D>();
        boxCollider2d = gameObject.GetComponent<BoxCollider2D>();
        initRotationTop =topPointer.transform.rotation;
        initRotationAim = aimPointer.transform.rotation;
        currentHealth = maxHealth;
        swingColdeDownTimer = 0.0f;
        InvincibleTimer = 0f;
        throwObject = 0;
        
    }
    void Start()
    {
        float ySize = MainCamera.orthographicSize;
        InitFruit();
        damageZoneSize = new Vector2(ySize * MainCamera.aspect + damageZoneOffSet, ySize + damageZoneOffSet);
    }

    // Update is called once per frame
    void Update()
    {
        //Check if on the ground
        // Debug.Log(transform.position);
        maxHeight = Mathf.Max(maxHeight, transform.position.y);
        if (Input.GetButtonDown(playerData.switchButton)) {
            SwitchItem();
        }
        
        if (isGrounded&&!isAimming&&Input.GetButtonDown(playerData.jumpButton))
            {
                //Debug.Log("Jump");
                Jump();
            }
        if (Input.GetButtonDown(playerData.throwButton))
        if (isGrounded && Input.GetButtonDown(playerData.throwButton)) {
                if (isAimming) { CancleAim(); } else { Aim(); }
        }
        if (isAimming && Input.GetButtonDown(playerData.jumpButton)) {
            Throw(throwAngle);
        }
        if (isSwinging && Input.GetButtonDown(playerData.jumpButton)) {
            //Swing(true);
            Swing();
        }
        if (useSwingTimer) {
            swingColdeDownTimer += Time.deltaTime;
            if (swingColdeDownTimer > maxSwingColdDownTime) {
                ResetSwingTimer();
            }
        }
        if (isInvincible) {
            InvincibleTimer += Time.deltaTime;
            if (InvincibleTimer >= maxInvincibleTime) {
                isInvincible = false;
                InvincibleTimer = 0f;
            }
        }
        //if (isGrounded && transform.position.y > levelPoint.transform.position.y)
        //{
        //    levelPoint.transform.position = new Vector3(0, transform.position.y,0);
        //}
        if (transform.position.y > levelPoint.transform.position.y)
        { 
           levelPoint.transform.position = new Vector3(0, transform.position.y,0);
        }
        //if (isSwinging && swingColdeDownTimer==0&&transform.position.y > levelPoint.transform.position.y) {
        //    levelPoint.transform.position = new Vector3(0, transform.parent.parent.parent.parent.position.y, 0);
        //}
        if (Mathf.Abs(transform.position.x - levelPoint.position.x) > damageZoneSize.x || levelPoint.position.y - transform.position.y > damageZoneSize.y)
        {
            if (!isInvincible)
            {
                changePlayerHealth(-1, true);
            }
            else {
                Invoke("reborn",rebornTime);
            }
        }
        if (rigidbody2d.velocity.x > 0)
        {
            sprite.flipX = false;
        }
        else if (rigidbody2d.velocity.x < 0)
        {
            sprite.flipX = true;
        }

    }
    private void FixedUpdate()
    {
        if (isAimming) {
            AimPointerRotate();
        }
        else if (isGrounded) {
            PointerRotate();
        }
       
    }
    void InitFruit() {
        for (int i = 0; i < fruitdatas.Count-cannotTrowFruitNum; i++) {
            fruitHold[i] = fruitdatas[i].initHold;
            UpdateFruitUI(i);
        }
    }
    void UpdateFruitUI(int i) {
        if (playerData.id == PlayerData.playerid.P1)
        {
            UIControl.instance.SetFruitValue(1, fruitdatas[i].id, fruitHold[i]);
        }
        else
        {
            UIControl.instance.SetFruitValue(2, fruitdatas[i].id, fruitHold[i]);
        }
    }
    void PointerRotate() {
        jumpAngle += jumpAngleChangeSpeed * jumpAngleChangeDirection;

        if (jumpAngle > jumpAngleRange / 2 || jumpAngle < -jumpAngleRange / 2)
        {
            jumpAngleChangeDirection *= -1;
        }
        topPointer.transform.Rotate(0, 0, jumpAngleChangeSpeed * jumpAngleChangeDirection);
    }
    void Jump() {
        //AddForce
        lastStandPosition = transform.position;
        float radians = (jumpAngle + 90) * (Mathf.PI / 180);
        rigidbody2d.AddForce(new Vector2(Mathf.Cos(radians), Mathf.Sin(radians)) * jumpForce, ForceMode2D.Impulse);

        //PlayJumpAnime
        animator.SetBool("Onground", false);
        animator.SetBool("Jump",true);
        //PlayJumpSoundTrack
        JumpAudio.Play();
        //Reset the Pointer
        ResetTopPointer();
        isGrounded = false;
    }
    private void ResetTopPointer()
    {
        jumpAngle = 0;
        topPointer.transform.rotation = initRotationTop;
        topPointer.SetActive(false);
        jumpArc.SetActive(false);
    }
    public void SetGrounded() {
            isGrounded = true;
            rigidbody2d.velocity = Vector2.zero;
            topPointer.SetActive(true);
            jumpArc.SetActive(true);
            animator.SetBool("Jump", false);
            animator.SetBool("Onground",true);
 
    }
    public void Grab(Transform grab,Vector3 OffSet) {
        grabVine = grab.GetChild(0)?.GetComponent<GrabVine>();
        lastStandPosition = grab.position;
        rigidbody2d.velocity = Vector2.zero;
        rigidbody2d.gravityScale = 0;
        boxCollider2d.enabled = false;
        transform.SetParent(grab);
        isGrounded = false;
        isSwinging = true;
        animator.SetBool("IsSwinging", true);
        animator.SetBool("Onground", false);
        animator.SetBool("Jump", false);
        
        transform.localRotation = Quaternion.Euler(Vector3.zero);
        transform.localPosition = OffSet;
        //transform.SetPositionAndRotation(new Vector3(0, -1, 0), Quaternion.Euler(Vector3.zero));
    }
    void Swing(bool isSwingOut) {
        
        rigidbody2d.gravityScale = 1;
        boxCollider2d.enabled = true;
        //VineControl vine = transform.parent.GetComponent<VineControl>();
     
        float swingAngle = transform.parent.GetComponent<VineControl>().SetSwinging(false);
        if (isSwingOut)
        {
            float radians = (swingAngle) * (Mathf.PI / 180);
            rigidbody2d.AddForce(new Vector2(Mathf.Cos(radians), Mathf.Sin(radians)) * swingForce, ForceMode2D.Impulse);
            SwingAudio.Play();
            animator.SetBool("IsSwinging",false);
        }
        //Debug.Log(swingAngle);
        transform.SetParent(playerParents);
        grabVine.cutProcess -= cutFall;
        isGrounded = false;
        useSwingTimer = true;
    }
    void Swing() {
        rigidbody2d.gravityScale = 1;
        boxCollider2d.enabled = true;
        float swingAngle = transform.parent.GetComponent<VineControl>().SetSwinging(false);
        float radians = (swingAngle) * (Mathf.PI / 180);
        rigidbody2d.AddForce(new Vector2(Mathf.Cos(radians), Mathf.Sin(radians)) * swingForce, ForceMode2D.Impulse);
        SwingAudio.Play();
        animator.SetBool("IsSwinging", false);
        transform.SetParent(playerParents);
        grabVine.cutProcess -= cutFall;

        useSwingTimer = true;
    }
    private void ResetSwingTimer()
    {
        useSwingTimer = false;
        swingColdeDownTimer = 0.0f;
        isSwinging = false;
    }
    void AimPointerRotate()
    {
        throwAngle += aimAngleChangeSpeed * aimAngleChangeDirection;

        if (throwAngle > aimAngleRange / 2 || throwAngle < -aimAngleRange / 2)
        {
            aimAngleChangeDirection *= -1;
        }
        aimPointer.transform.Rotate(0, 0, aimAngleChangeSpeed * aimAngleChangeDirection);
    }
    private void ResetAimPointer()
    {
        isAimming = false;
        throwAngle = 0;
        aimPointer.transform.rotation = initRotationTop;
        aimPointer.SetActive(false);
        aimArc.SetActive(false);
        aimAudio.Play();
    }
    void Aim() {
        isAimming = true;
        ResetTopPointer();
        aimPointer.SetActive(true);
        aimArc.SetActive(true);
        aimAudio.Play();
    }
    void CancleAim() {
        ResetAimPointer();
        topPointer.SetActive(true);
        jumpArc.SetActive(true);
        isGrounded = true;
    }
    void Throw(float tAngle) {
        float radians = (tAngle + 90) * (Mathf.PI / 180);
        if (fruitHold[throwObject] >= 1)
        {
            GameObject fruit = Instantiate(fruitPrefabs[throwObject], transform.position, Quaternion.identity);
            //if (fruitdatas[throwObject].isRotateWhenThrow)
            //{
            fruit.GetComponent<Rigidbody2D>()?.AddForce(new Vector2(Mathf.Cos(radians), Mathf.Sin(radians)) * aimForce, ForceMode2D.Impulse);
            
            //}
            fruitHold[throwObject] -= 1;
            UpdateFruitUI(throwObject);
            ThrowAudio.Play();
        }
    }
    void SwitchItem() {
        throwObject += 1;
        if (throwObject >= fruitdatas.Count)
        {
            throwObject = 0;
        }
        if (playerData.id == PlayerData.playerid.P1) {
            UIControl.instance.SwitchFruitUIP1();
        }else
        {
            UIControl.instance.SwitchFruitUIP2();
        }

    }
    public void changePlayerHealth(int changValue,bool isoutofbox) {
        currentHealth = Mathf.Clamp(currentHealth + changValue, 0, maxHealth);

        if (changValue < 0) {
            damaged();
            if (isoutofbox)
            {
                Invoke("reborn", rebornTime);
                fallAudio.Play();
            }
            else {
                damageAuido.Play();
            }
            if (currentHealth <= 0) {
                GameMananement.instance.GameOver();

            }
        }
    }
    void reborn() {
        transform.parent.GetComponent<VineControl>()?.SetSwinging(false);
        transform.SetParent(playerParents);
        transform.localRotation = Quaternion.Euler(Vector3.zero);
        rigidbody2d.velocity = Vector2.zero;
        isSwinging = false;
        swingColdeDownTimer = 0;
        rigidbody2d.gravityScale = 1;
        boxCollider2d.enabled = true;
        transform.position = new Vector3(levelPoint.position.x, levelPoint.position.y , 0);
        //isGrounded = true;
        animator.SetBool("Jump", false);
        animator.SetBool("IsSwinging", false);
        animator.SetBool("Onground", true);
    }
    void damaged() {
           
            if (isInvincible)
                return;
            //Playsound
            //SetTrigger
            //ChangeSprite
            //SetInvincible
            isInvincible = true;
            InvincibleTimer = 0f;
            SetHealthUI();
            animator.SetBool("Jump", false);
            animator.SetBool("IsSwinging", false);
            animator.SetBool("Onground", true);
    }
    void SetHealthUI (){

        if (playerData.id == PlayerData.playerid.P1)
            {
                HealthControl.instance.SetP1Value(currentHealth / (float) maxHealth);
}
            else {
                HealthControl.instance.SetP2Value(currentHealth / (float) maxHealth);
            }
    }
    public void cutFall() {
        Swing(false);
        changePlayerHealth(-1,false);
        grabVine.cutProcess -= cutFall;
    }
    public bool PickUpFruit(FruitData fruitdata) {
        if (fruitdata.isThrowable) {
            if (fruitHold[fruitdata.id] + 1 < fruitdata.maxHold)
            {
                fruitHold[fruitdata.id] += 1;
                UpdateFruitUI(fruitdata.id);
                PickUpFruitAudio.Play();
                return true;
            }
            else { return false; }
            
        }
        else if (fruitdata.addHealth) {
            if (currentHealth + 1 < maxHealth)
            {
                changePlayerHealth(1,false);
                SetHealthUI();
                AddHealthAudio.Play();
                return true;
            }
            else { return false; }
        }
        return false;
    }
    public int getHealth() {
        return currentHealth;
    }
}
