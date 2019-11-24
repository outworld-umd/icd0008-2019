using System;

namespace MenuSystem {

    public class MenuItem {

        public string? Title { get; set; }

        public string? Command { get; set; }

        public Func<string>? CommandToExecute { get; set; }

        public override string ToString() {
            return Command + " " + Title;
        }
    }
}