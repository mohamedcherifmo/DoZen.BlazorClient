using System.Text.Json;

namespace DoZen.BlazorClient.Data
{
    public class StateContainer
    {
        public string NavigationItem { get; set; }

        public string GetStateForLocalStorage()
        {
            return JsonSerializer.Serialize(this);
        }

        public void SetStateFromLocalStorage(string locallyStoredState)
        {
            var deserializedState =
                JsonSerializer.Deserialize<StateContainer>(locallyStoredState);

            NavigationItem = deserializedState.NavigationItem;
        }
    }
}
