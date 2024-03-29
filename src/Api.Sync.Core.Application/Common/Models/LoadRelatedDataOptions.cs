﻿using Api.Core.Domain.Common;

namespace Api.Sync.Core.Application.Common.Models;

public sealed class LoadRelatedDataOptions : ILoadRelatedDataOptions
{
    public static readonly LoadRelatedDataOptions Default = new() { CargarDatosExtra = false };

    public bool CargarDatosExtra { get; set; }
}
