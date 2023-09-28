using System;
using System.Collections.Generic;

namespace YouTubeVideoSuggestionApp.Models;

public partial class Suggestion
{
    public int Id { get; set; }

    public string Username { get; set; } = null!;

    public string? ShortDescription { get; set; }

    public string Password { get; set; } = null!;

    public string YouTubeUrl { get; set; } = null!;

    public bool? Status { get; set; }

    public DateTime LastModified { get; set; }
}
