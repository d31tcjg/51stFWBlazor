namespace _51stBlazor.Discord
{
    using DSharpPlus;
    using System.Threading.Tasks;
    using System;
    using System.Threading;
    using System.Threading.Tasks;
    using Microsoft.Extensions.Hosting;
    using DSharpPlus;
    using DSharpPlus.Entities;
    using System.Data;

    public class DiscordBot : IHostedService
    {
        private readonly DiscordClient _client;
        public bool IsReady { get; private set; }


        public DiscordBot(string token)
        {

            var config = new DiscordConfiguration
            {
                Token = token,
                TokenType = TokenType.Bot,
                Intents = DiscordIntents.GuildMembers | DiscordIntents.Guilds // the other intents your bot needs
            };
            _client = new DiscordClient(config);

            _client.Ready += OnClientReady;
        }

        private Task OnClientReady(DiscordClient sender, DSharpPlus.EventArgs.ReadyEventArgs e)
        {
            IsReady = true;


            return Task.CompletedTask;
        }

        public async Task StartAsync(CancellationToken cancellationToken)
        {
            await _client.ConnectAsync();
        }

        public async Task StopAsync(CancellationToken cancellationToken)
        {
            await _client.DisconnectAsync();
        }

        public IReadOnlyDictionary<ulong, DiscordGuild> GetGuilds()
        {
            return _client.Guilds; // Returns a dictionary of guilds the bot is connected to.
        }


        public string GetBotUsername()
        {
            return IsReady ? _client.CurrentUser.Username : null;

        }

        public async Task<IEnumerable<DiscordMember>> GetGuildMembers(ulong guildId)
        {
            var guild = await _client.GetGuildAsync(guildId);
            var members = await guild.GetAllMembersAsync();
            return members;
        }

        public async Task<IEnumerable<DiscordMessage>> ReadChannelContentsAsync(ulong channelId)
        {
            // Get the channel from the guild
            var channel = await _client.GetChannelAsync(channelId) as DiscordChannel;

            // Ensure we got a text channel
            if (channel == null || channel.Type != ChannelType.Text)
            {
                Console.WriteLine("Invalid channel type!");
                return null;
            }

            // Get the messages from the channel
            var messages = await channel.GetMessagesAsync();

            return messages;
        }

    }



}
