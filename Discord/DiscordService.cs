using DSharpPlus.Entities;
using System;

namespace _51stBlazor.Discord;

public class DiscordService
{
    private readonly DiscordBot _bot;

    public DiscordService(DiscordBot bot)
    {
        _bot = bot;
    }

    public IReadOnlyDictionary<ulong, DiscordGuild> GetGuilds()
    {
        return _bot.GetGuilds();
    }

    public bool IsReady => _bot.IsReady;


    public string GetBotUsername()
    {
        return _bot.GetBotUsername();
    }

    public async Task<IEnumerable<DiscordMember>> GetGuildMembers(ulong guildId)
    {
        return await _bot.GetGuildMembers(guildId);
    }

}
