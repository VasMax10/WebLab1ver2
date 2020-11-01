using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using WebApp.Models;
using Telegram.Bot;
using System.Globalization;
using Telegram.Bot.Types.ReplyMarkups;

namespace WebApp.Controllers
{
    public class BotTelegram
    {
        private readonly TelegramBotClient Bot = 
            new TelegramBotClient("1272477139:AAFAGr2Ogfus4vdm6YGAgf1p3YZRDB1IoFs");
        private readonly FandomContext _context;
        public BotTelegram(FandomContext context)
        {
            _context = context;
            Bot.OnMessage += Bot_OnMessage;
            Bot.OnCallbackQuery += Bot_OnCallBack;
            // Bot.OnMessageEdited += Bot_OnMessage;

            Bot.StartReceiving();
        }
        private void Bot_OnCallBack(object sender, Telegram.Bot.Args.CallbackQueryEventArgs e)
        {
            Bot.SendTextMessageAsync(e.CallbackQuery.From.Id, e.CallbackQuery.Data + "\n" +
                    "Incorrect input. To see available commands try /info");   
            
            
        }

        private static InlineKeyboardButton[][] GetInlineKeyboard(string [] stringArray)
        {
            var keyboardInline = new InlineKeyboardButton[1][];
            var keyboardButtons = new InlineKeyboardButton[stringArray.Length];
                for (var i = 0; i < stringArray.Length; i++)
                {
                    keyboardButtons[i] = new InlineKeyboardButton
                    {
                        Text = stringArray[i],
                        CallbackData = "Some Callback Data",
                    };
                }
            keyboardInline[0] = keyboardButtons;
            return keyboardInline;
        }
        private void Bot_OnMessage(object sender, Telegram.Bot.Args.MessageEventArgs e)
        {   
            if (e.Message.Type == Telegram.Bot.Types.Enums.MessageType.Text)
            {
                switch (e.Message.Text)
                {
                    case "/start":
                        Bot.SendTextMessageAsync(e.Message.Chat.Id, "Hi, " + e.Message.Chat.Username);
                        break;
                    case "/coord":
                        string name = _context.Actors.FirstOrDefault().Name;
                        float lat = float.Parse(_context.Actors.FirstOrDefault().Lat, CultureInfo.InvariantCulture);
                        float lng = float.Parse(_context.Actors.FirstOrDefault().Lng, CultureInfo.InvariantCulture);
                        Bot.SendVenueAsync(e.Message.Chat.Id, lat, lng, name, "");
                        break;
                    case "/button":
                        var items = _context.Series.Select(s => s.Name).ToArray();
                        // var buttonItem = new[] { "one", "two", "three", "Four" };
                        var keyboardMarkup = new InlineKeyboardMarkup(GetInlineKeyboard(items));
                        Bot.SendTextMessageAsync(e.Message.Chat.Id, "-->", replyMarkup: keyboardMarkup);   
                        // Bot.SendTextMessageAsync(e.Message.Chat.Id, "Smth", );
                        break;
                    case "How are you?":
                        Bot.SendTextMessageAsync(e.Message.Chat.Id, "Fine, thank you) And you?");
                        break;
                    case "Good morning)":
                        Bot.SendTextMessageAsync(e.Message.Chat.Id, "Good morning, " + e.Message.Chat.Username);
                        break;
                    case "/series":
                        Bot.SendTextMessageAsync(e.Message.Chat.Id, "Good morning, " + e.Message.Chat.Username);
                        break;
                    case "/info":
                        Bot.SendTextMessageAsync(e.Message.Chat.Id, "Good morning, " + e.Message.Chat.Username);
                        break;
                    default:
                        Bot.SendTextMessageAsync(e.Message.Chat.Id, e.Message.Text + "\n" +
                        "Incorrect input. To see available commands try /info");
                        break;

                }
            } 
        }
    }
}