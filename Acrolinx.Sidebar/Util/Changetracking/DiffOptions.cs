/* Copyright (c) 2024 Acrolinx GmbH */

namespace Acrolinx.Sdk.Sidebar.Util.Changetracking
{
    public class DiffOptions
    {
        /*
         * Set how many seconds any diff's exploration phase may take.
         */
        public int diffTimeoutInSeconds { get; set; } = 5;

        /*
         * Set the format of original text.
         * This is useful when diffing markup content with innerText
         */
        public DiffInputFormat diffInputFormat { get; set; } = DiffInputFormat.TEXT;

    }
}
