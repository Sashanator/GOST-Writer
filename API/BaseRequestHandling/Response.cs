using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Domain;

public class Response
{
    /// <summary>
    /// </summary>
    protected bool _hasError;

    /// <summary>
    /// </summary>
    /// <param name="id">Should be the same value as the Id property of the Request that generated this Response.</param>
    public Response(Guid id)
    {
        Id = id;
    }

    /// <summary>
    ///     A Response id that should be used as an idempontency code. Should be the same value as the Id property of the
    ///     Request that generated this Response.
    /// </summary>
    public Guid Id { get; protected set; }

    /// <summary>
    ///     A status code for this reponse. Normally, Http status codes should be used.
    /// </summary>
    public int StatusCode { get; set; }

    /// <summary>
    ///     The type of the Response payload
    /// </summary>
    [JsonIgnore]
    public Type PayloadType => Payload?.GetType();

    /// <summary>
    ///     The payload of the response. It is a non generic 'object' to make it easier to register the Mediatr pipeline.
    ///     If knowing the payload type is necessary, refer to the PayloadType property.
    /// </summary>
    public object Payload { get; set; }

    /// <summary>
    ///     If the request resulted in an Exception, return that exception in the response object on this property, not the
    ///     Payload.
    /// </summary>
    [JsonIgnore]
    public Exception Exception { get; set; }

    /// <summary>
    ///     Any additonal message.
    /// </summary>
    public IList<string> Messages { get; set; } = new List<string>();

    /// <summary>
    ///     Checks if the Response resulted in some error.
    /// </summary>
    public bool HasError
    {
        get => Exception != null || _hasError;
        set => _hasError = value;
    }

    /// <summary>
    ///     Any other necessary information for the Response.
    /// </summary>
    public Dictionary<string, string> Meta { get; set; } = new();

    public static Response InternalServerError(Guid id, Exception ex = null)
    {
        return new ErrorResponse(id, ex);
    }

    public static Response Ok(Guid id, object payload = null)
    {
        return new SuccessResponse(id, payload);
    }

    public static Response BadRequest(Guid id, Exception ex = null, Dictionary<string, string> messages = null)
    {
        return new ErrorResponse(id, messages, ex, 400);
    }
}