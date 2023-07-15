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

    public IEnumerable<DiscordMember> Members { get; private set; }

    public async Task FetchMembers(ulong guildId)
    {
        Members = await _bot.GetGuildMembers(guildId);
    }

    public async Task<IEnumerable<DiscordMessage>> ReadChannelContentsAsync(ulong channelId)
    {
        return await _bot.ReadChannelContentsAsync(channelId);
    }

}
