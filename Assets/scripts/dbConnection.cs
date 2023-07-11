using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Firebase.Database;
using TMPro;
public class dbConnection : MonoBehaviour
{
    public GameObject first;
    public GameObject second;
    public GameObject third;
    public GameObject fourth;
    public GameObject fifth;
    public GameObject sixth;
    public List<Activity> activities;
    public Transform content;
    public GameObject activityPrefab;
    public TextMeshPro activityLabel;
    [SerializeField] Data data;
    public TMP_InputField activityTitle;
    public TMP_InputField duration;
    public TMP_Dropdown priority;
    DatabaseReference db;
    public TMP_InputField email;
    AuthManager authscript;
    public GameObject authManager;
    string user_id;
    private bool dataExists;
    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("before");
        db = FirebaseDatabase.GetInstance("https://productivity-app-7a77c-default-rtdb.firebaseio.com/").RootReference;
        Debug.Log("after");
        authscript = authManager.GetComponent<AuthManager>();
        user_id = authscript.User.UserId;
        Debug.Log(user_id);
        StartCoroutine(fetchActivitiess());
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void addTask()
    {
       
        StartCoroutine(addActivity());
        
        

        
       
        // Debug.Log(userId);
        Debug.Log(JsonUtility.ToJson(this.data.activities));
        Debug.Log("task added");
    }
    public void fetchActivities()
    {
        StartCoroutine(fetchActivitiess());
    }
    private IEnumerator addActivity()
    {
        Debug.Log("User ID is: ");
        Debug.Log(user_id);
        var userref = db.Child("users").Child(this.user_id).GetValueAsync();
        yield return new WaitUntil(predicate: () => userref.IsCompleted);
        if (userref.Exception != null)
        {
            Debug.LogWarning(message: $"Failed to fetch data with{userref.Exception} ");
        }
        else if (userref.Result.Value == null)
        {
            
            string priorityVal = priority.options[priority.value].text;
            Debug.Log("its null");
            Activity activity = new Activity(0, activityTitle.text, int.Parse(duration.text), priorityVal);
            string json1 = JsonUtility.ToJson(activity);
            Activity[] acts = { activity };
            data = new Data(1, acts);
            string json = JsonUtility.ToJson(this.data);
            db.Child("users").Child(user_id).SetRawJsonValueAsync(json);

        }
        else
        {

            DataSnapshot result = userref.Result;
            string x = System.Convert.ToString(result.Child("maxId").Value) ;
            int i = int.Parse(x);
            int y = 1 + i;
            string maxId = y.ToString();
            Debug.Log(y);
            string priorityVal = priority.options[priority.value].text;
            Activity activity = new Activity(y, activityTitle.text, int.Parse(duration.text), priorityVal);
            string json = JsonUtility.ToJson(activity);
            db.Child("users").Child(this.user_id).Child("activities").Child(maxId).SetRawJsonValueAsync(json); 

            db.Child("users").Child(this.user_id).Child("maxId").SetValueAsync(y);
            var acts = result.Child("activities").Child("0").Child("activityTitle").Value;
            Debug.Log(acts);
            Debug.Log("NOT null");
            StartCoroutine(fetchActivitiess()); 
        }
        

    }
    public void handleClickfirst() {
        activities.RemoveAt(0);
        string json = JsonUtility.ToJson(activities);
        Debug.Log(json);
        db.Child("users").Child(this.user_id).Child("activities").SetRawJsonValueAsync(json);
    }
    public void handleClicksecond()
    {
        activities.RemoveAt(1);
        string json = JsonUtility.ToJson(activities);
        db.Child("users").Child(this.user_id).Child("activities").SetRawJsonValueAsync(json);
    }
    public void handleClickthird()
    {
        activities.RemoveAt(2);
        string json = JsonUtility.ToJson(activities);
        db.Child("users").Child(this.user_id).Child("activities").SetRawJsonValueAsync(json);
    }
    public void handleClickfourth()
    {
        activities.RemoveAt(3);
        string json = JsonUtility.ToJson(activities);
        db.Child("users").Child(this.user_id).Child("activities").SetRawJsonValueAsync(json);
    }
    public void handleClickfifth()
    {
        activities.RemoveAt(4);
        string json = JsonUtility.ToJson(activities);
        db.Child("users").Child(this.user_id).Child("activities").SetRawJsonValueAsync(json);
    }
    public void handleClicksixth()
    {
        activities.RemoveAt(5);
        string json = JsonUtility.ToJson(activities);
        db.Child("users").Child(this.user_id).Child("activities").SetRawJsonValueAsync(json);
    }

    private IEnumerator fetchActivitiess()
    {
        var userref = db.Child("users").Child(this.user_id).GetValueAsync();
        yield return new WaitUntil(predicate: () => userref.IsCompleted);
        yield return new WaitUntil(predicate: () => userref.IsCompleted);
        if (userref.Exception != null)
        {
            Debug.LogWarning(message: $"Failed to fetch data with{userref.Exception} ");
        }
        else if (userref.Result.Value == null)
        {

            Debug.Log("empty records");

        }
        else
        {
            
            DataSnapshot snapshot =userref.Result.Child("activities");
            // Do something with snapshot...
            
            foreach (DataSnapshot s in snapshot.Children)
            {
                

                IDictionary dictUsers = (IDictionary)s.Value;
                Activity activity = new Activity(int.Parse(dictUsers["id"].ToString()), dictUsers["activityTitle"].ToString(), int.Parse(dictUsers["duration"].ToString()), dictUsers["priority"].ToString());
                activities.Add(activity);
                
              
                //Debug.Log(dictUsers["id"].GetType());

            }
            int count = activities.Count;
            if (count > 0)
            {
                first.SetActive(true);
                first.GetComponentInChildren<TextMeshProUGUI>().text = activities[0].activityTitle;
                count = count - 1;
            }
            if (count > 0)
            {
                second.SetActive(true);
                second.GetComponentInChildren<TextMeshProUGUI>().text = activities[1].activityTitle;
                count = count - 1;
            }
            if (count > 0)
            {
                third.SetActive(true);
                third.GetComponentInChildren<TextMeshProUGUI>().text = activities[2].activityTitle;
                count = count - 1;
            }
            if (count > 0)
            {
                fourth.SetActive(true);
                fourth.GetComponentInChildren<TextMeshProUGUI>().text = activities[3].activityTitle;
                count = count - 1;
            }
            if (count > 0)
            {
                fifth.SetActive(true);
                fifth.GetComponentInChildren<TextMeshProUGUI>().text = activities[4].activityTitle;
                count = count - 1;
            }
            if (count > 0)
            {
                sixth.SetActive(true);
                sixth.GetComponentInChildren<TextMeshProUGUI>().text = activities[5].activityTitle;
                count = count - 1;
            }

        }

    }
}
[System.Serializable]
public class Activity {
    public int id;
    public string activityTitle;
    public int duration;
    public int finished_duration;
    public string priority;
    public Activity() {
    }
    public Activity(int id, string activity, int duration, string priority) {
        this.id = id;
        this.activityTitle = activity;
        this.duration = duration;
        this.priority = priority;
    }
    public void setDuration(int finished_duration) {
        this.finished_duration = finished_duration;
    }
   
}
[System.Serializable]
class Data {
    public Activity[] activities;
    public int maxId;
    public Data() {
    }
    public Data(int maxId, Activity[] activities) {
        this.maxId = maxId;
        this.activities = activities;
    }
}