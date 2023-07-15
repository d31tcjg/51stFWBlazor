using DSharpPlus.Entities;

namespace _51stBlazor.Discord
{
    public class DiscordRosterService
    {

        private readonly DiscordService _discordService;


        public DiscordRosterService(DiscordService discordService)
        {
            _discordService = discordService;
        }

        public async Task<IEnumerable<DiscordMessage>> LoadChannelContentsAsync(ulong channelId)
        {
            return await _discordService.ReadChannelContentsAsync(channelId);
        }

        public async Task<Dictionary<string, string>> CreateRosterAsync(ulong channelId)
        {
            var messages = await LoadChannelContentsAsync(channelId);
            string content = messages.Last().Content;
            var lines = content.Split(new[] { '\r', '\n' }, StringSplitOptions.RemoveEmptyEntries);
            var roster = new Dictionary<string, string>();

            foreach (var line in lines)
            {
                if (!line.Contains('|'))
                {
                    continue;
                }

                var parts = line.Split('|');
                var key = parts[0].Trim();
                var value = parts.Length > 1 ? parts[1].Trim() : string.Empty;
                roster[key] = value;
            }

            return roster;
        }

        public List<Pilot> GeneratePilots(IEnumerable<DiscordMember> members, Dictionary<string, string> roster)
        {
            var pilots = new List<Pilot>();

            foreach (var rosterEntry in roster)
            {
                foreach (var member in members)
                {
                    var displayNameParts = member.DisplayName.Split(' ');
                    var match = rosterEntry.Value.ToLower().Contains(displayNameParts[^1].ToLower());

                    if (match)
                    {
                        var p = new Pilot { Name = rosterEntry.Value, Aircraft = rosterEntry.Key, AvatarUrl = member.AvatarUrl };
                        pilots.Add(p);
                        break;  // if a match is found, break the inner loop
                    }
                }
            }

            return pilots;
        }

    }
}
