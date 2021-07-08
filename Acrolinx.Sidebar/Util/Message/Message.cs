/* Copyright (c) 2021 Acrolinx GmbH */

namespace Acrolinx.Sdk.Sidebar.Util.Message
{
    public class Message
    {
        public MessageType Type { get; private set; }
        public string Title { get; private set; }
        public string Text { get; private set; }

        public Message(MessageType type, string title, string text)
        {
            Type = type;
            Title = title;
            Text = text;
        }
    }
}
