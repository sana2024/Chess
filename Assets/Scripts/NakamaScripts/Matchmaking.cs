using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Nakama.TinyJson;
using System.Linq;
using Nakama;
using UnityEngine.SceneManagement;
using System;

public class Matchmaking : MonoBehaviour
{

    [SerializeField] Text Searching;
    string matchid;
    IUserPresence hostPresence;
    IUserPresence SecondPresence;

    public async void findMatch()
    {
        var properity = new Dictionary<string, string>() {{"board", "cualalampur"} };
        var query = "+properties.board:" + "cualalampur";
        await PassData.socket.AddMatchmakerAsync(query,2,2,properity );
        Searching.text = "searching";
        Debug.Log("searching");
    }



   public void Start()
    {

        var mainThread = UnityMainThreadDispatcher.Instance();
        PassData.socket.ReceivedMatchmakerMatched += match => mainThread.Enqueue(() => OnREceivedMatchmakerMatched(match));
        PassData.socket.ReceivedMatchPresence += m => mainThread.Enqueue(() => OnRecivedMatchPresence(m));
 
 

    }

    private async void OnRecivedMatchPresence(IMatchPresenceEvent m)
    {
        foreach (var presence in m.Joins)
        {
            Searching.text = "Found";
            
        }
    }

    private async void OnREceivedMatchmakerMatched(IMatchmakerMatched matchmakerMatched)
    {
        var match = await PassData.socket.JoinMatchAsync(matchmakerMatched);
        matchid = matchmakerMatched.MatchId;
        Searching.text = "Found";
        Debug.Log("Joined match " + match.Id);

        hostPresence = matchmakerMatched.Users.OrderBy(x => x.Presence.SessionId).First().Presence;
        SecondPresence = matchmakerMatched.Users.OrderBy(x => x.Presence.SessionId).Last().Presence;

        PassData.hostPresence = hostPresence.UserId;
        PassData.SecondPresence = SecondPresence.UserId;

        PassData.match = match;

        SceneManager.LoadScene("chess");
    }
 
}
