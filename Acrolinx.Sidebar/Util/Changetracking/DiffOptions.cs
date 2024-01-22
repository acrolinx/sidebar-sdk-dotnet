/* Copyright (c) 2024 Acrolinx GmbH */

namespace Acrolinx.Sdk.Sidebar.Util.Changetracking
{
    public class DiffOptions
    {
        public int diffTimeout { get; set; } = 5; // seconds
        public int diffEditCost { get; set; } = 4;
        public DiffInputFormat diffInputFormat { get; set; } = DiffInputFormat.TEXT;

    }
}
