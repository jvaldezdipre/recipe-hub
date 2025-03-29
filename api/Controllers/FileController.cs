using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using api.Services;
using System;
using System.Linq;
using System.Net.Http;

namespace api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FileController : ControllerBase
    {
        private readonly IFileStorageService _fileStorage;

        public FileController(IFileStorageService fileStorage)
        {
            _fileStorage = fileStorage;
        }

        /// <summary>
        /// Uploads a file to the server.
        /// </summary>
        /// <param name="file">The file to upload</param>
        /// <returns>The URL of the uploaded file</returns>
        [HttpPost("upload")]
        [Authorize]
        public async Task<IActionResult> UploadFile(IFormFile file)
        {
            if (file == null || file.Length == 0)
            {
                return BadRequest("No file was uploaded");
            }

            try
            {
                // Validate file size (e.g., limit to 5 MB)
                if (file.Length > 5 * 1024 * 1024)
                {
                    return BadRequest("File size exceeds the limit (5 MB)");
                }

                // Validate file type (only images)
                var allowedTypes = new[] { "image/jpeg", "image/png", "image/gif", "image/webp", "image/avif" };
                if (!allowedTypes.Contains(file.ContentType.ToLower()))
                {
                    return BadRequest("Only image files are allowed");
                }

                // Save the file
                var fileUrl = await _fileStorage.SaveFileAsync(file);
                
                return Ok(new { url = fileUrl });
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred while uploading the file: {ex.Message}");
            }
        }

        /// <summary>
        /// Validates and accepts an external image URL
        /// </summary>
        /// <param name="imageUrl">The external image URL</param>
        /// <returns>The validated URL if successful</returns>
        [HttpPost("external-url")]
        [Authorize]
        public async Task<IActionResult> ValidateExternalUrl([FromBody] ExternalUrlDto urlDto)
        {
            if (string.IsNullOrEmpty(urlDto.Url))
            {
                return BadRequest("No URL was provided");
            }

            try
            {
                // Validate URL format
                if (!Uri.TryCreate(urlDto.Url, UriKind.Absolute, out Uri? uri))
                {
                    return BadRequest("Invalid URL format");
                }

                // Validate URL scheme
                if (uri.Scheme != "http" && uri.Scheme != "https")
                {
                    return BadRequest("URL must use HTTP or HTTPS");
                }

                // Optional: Validate that it's an image URL by checking common image extensions
                var imageExtensions = new[] { ".jpg", ".jpeg", ".png", ".gif", ".webp", ".avif" };
                var path = uri.AbsolutePath.ToLowerInvariant();
                if (!imageExtensions.Any(ext => path.EndsWith(ext)))
                {
                    return BadRequest("URL must point to an image file (jpg, jpeg, png, gif, webp, avif)");
                }

                // Optional: You could make a HEAD request to validate the URL exists and returns an image
                using (var httpClient = new HttpClient())
                {
                    httpClient.Timeout = TimeSpan.FromSeconds(5);
                    var response = await httpClient.GetAsync(uri);
                    if (!response.IsSuccessStatusCode)
                    {
                        return BadRequest("Unable to access the provided URL");
                    }

                    if (!response.Content.Headers.ContentType?.MediaType?.StartsWith("image/") ?? true)
                    {
                        return BadRequest("The URL does not point to an image");
                    }
                }

                return Ok(new { url = urlDto.Url });
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred while validating the URL: {ex.Message}");
            }
        }
    }

    public class ExternalUrlDto
    {
        public string Url { get; set; } = string.Empty;
    }
} 