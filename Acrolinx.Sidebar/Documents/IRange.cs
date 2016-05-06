/* Copyright (c) 2016 Acrolinx GmbH */

using System;
namespace Acrolinx.Sdk.Sidebar.Documents
{
    public interface IRange
    {
        int End { get; }
        int Length { get; }
        int Start { get; }
    }
}
