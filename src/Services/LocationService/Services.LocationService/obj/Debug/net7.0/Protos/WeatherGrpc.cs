// <auto-generated>
//     Generated by the protocol buffer compiler.  DO NOT EDIT!
//     source: Protos/weather.proto
// </auto-generated>
#pragma warning disable 0414, 1591, 8981, 0612
#region Designer generated code

using grpc = global::Grpc.Core;

namespace DataCaptureService {
  public static partial class GrpcWeather
  {
    static readonly string __ServiceName = "GrpcWeather";

    [global::System.CodeDom.Compiler.GeneratedCode("grpc_csharp_plugin", null)]
    static void __Helper_SerializeMessage(global::Google.Protobuf.IMessage message, grpc::SerializationContext context)
    {
      #if !GRPC_DISABLE_PROTOBUF_BUFFER_SERIALIZATION
      if (message is global::Google.Protobuf.IBufferMessage)
      {
        context.SetPayloadLength(message.CalculateSize());
        global::Google.Protobuf.MessageExtensions.WriteTo(message, context.GetBufferWriter());
        context.Complete();
        return;
      }
      #endif
      context.Complete(global::Google.Protobuf.MessageExtensions.ToByteArray(message));
    }

    [global::System.CodeDom.Compiler.GeneratedCode("grpc_csharp_plugin", null)]
    static class __Helper_MessageCache<T>
    {
      public static readonly bool IsBufferMessage = global::System.Reflection.IntrospectionExtensions.GetTypeInfo(typeof(global::Google.Protobuf.IBufferMessage)).IsAssignableFrom(typeof(T));
    }

    [global::System.CodeDom.Compiler.GeneratedCode("grpc_csharp_plugin", null)]
    static T __Helper_DeserializeMessage<T>(grpc::DeserializationContext context, global::Google.Protobuf.MessageParser<T> parser) where T : global::Google.Protobuf.IMessage<T>
    {
      #if !GRPC_DISABLE_PROTOBUF_BUFFER_SERIALIZATION
      if (__Helper_MessageCache<T>.IsBufferMessage)
      {
        return parser.ParseFrom(context.PayloadAsReadOnlySequence());
      }
      #endif
      return parser.ParseFrom(context.PayloadAsNewBuffer());
    }

    [global::System.CodeDom.Compiler.GeneratedCode("grpc_csharp_plugin", null)]
    static readonly grpc::Marshaller<global::DataCaptureService.AirPollutionModelRequest> __Marshaller_AirPollutionModelRequest = grpc::Marshallers.Create(__Helper_SerializeMessage, context => __Helper_DeserializeMessage(context, global::DataCaptureService.AirPollutionModelRequest.Parser));
    [global::System.CodeDom.Compiler.GeneratedCode("grpc_csharp_plugin", null)]
    static readonly grpc::Marshaller<global::DataCaptureService.AirPollutionModelResponse> __Marshaller_AirPollutionModelResponse = grpc::Marshallers.Create(__Helper_SerializeMessage, context => __Helper_DeserializeMessage(context, global::DataCaptureService.AirPollutionModelResponse.Parser));
    [global::System.CodeDom.Compiler.GeneratedCode("grpc_csharp_plugin", null)]
    static readonly grpc::Marshaller<global::DataCaptureService.CurrentWeatherModelRequest> __Marshaller_CurrentWeatherModelRequest = grpc::Marshallers.Create(__Helper_SerializeMessage, context => __Helper_DeserializeMessage(context, global::DataCaptureService.CurrentWeatherModelRequest.Parser));
    [global::System.CodeDom.Compiler.GeneratedCode("grpc_csharp_plugin", null)]
    static readonly grpc::Marshaller<global::DataCaptureService.CurrentWeatherModelResponse> __Marshaller_CurrentWeatherModelResponse = grpc::Marshallers.Create(__Helper_SerializeMessage, context => __Helper_DeserializeMessage(context, global::DataCaptureService.CurrentWeatherModelResponse.Parser));
    [global::System.CodeDom.Compiler.GeneratedCode("grpc_csharp_plugin", null)]
    static readonly grpc::Marshaller<global::DataCaptureService.DailyWeatherModelRequest> __Marshaller_DailyWeatherModelRequest = grpc::Marshallers.Create(__Helper_SerializeMessage, context => __Helper_DeserializeMessage(context, global::DataCaptureService.DailyWeatherModelRequest.Parser));
    [global::System.CodeDom.Compiler.GeneratedCode("grpc_csharp_plugin", null)]
    static readonly grpc::Marshaller<global::DataCaptureService.DailyWeatherModelResponse> __Marshaller_DailyWeatherModelResponse = grpc::Marshallers.Create(__Helper_SerializeMessage, context => __Helper_DeserializeMessage(context, global::DataCaptureService.DailyWeatherModelResponse.Parser));

    [global::System.CodeDom.Compiler.GeneratedCode("grpc_csharp_plugin", null)]
    static readonly grpc::Method<global::DataCaptureService.AirPollutionModelRequest, global::DataCaptureService.AirPollutionModelResponse> __Method_AirPollution = new grpc::Method<global::DataCaptureService.AirPollutionModelRequest, global::DataCaptureService.AirPollutionModelResponse>(
        grpc::MethodType.Unary,
        __ServiceName,
        "AirPollution",
        __Marshaller_AirPollutionModelRequest,
        __Marshaller_AirPollutionModelResponse);

    [global::System.CodeDom.Compiler.GeneratedCode("grpc_csharp_plugin", null)]
    static readonly grpc::Method<global::DataCaptureService.CurrentWeatherModelRequest, global::DataCaptureService.CurrentWeatherModelResponse> __Method_CurrentWeather = new grpc::Method<global::DataCaptureService.CurrentWeatherModelRequest, global::DataCaptureService.CurrentWeatherModelResponse>(
        grpc::MethodType.Unary,
        __ServiceName,
        "CurrentWeather",
        __Marshaller_CurrentWeatherModelRequest,
        __Marshaller_CurrentWeatherModelResponse);

    [global::System.CodeDom.Compiler.GeneratedCode("grpc_csharp_plugin", null)]
    static readonly grpc::Method<global::DataCaptureService.DailyWeatherModelRequest, global::DataCaptureService.DailyWeatherModelResponse> __Method_DailyWeather = new grpc::Method<global::DataCaptureService.DailyWeatherModelRequest, global::DataCaptureService.DailyWeatherModelResponse>(
        grpc::MethodType.Unary,
        __ServiceName,
        "DailyWeather",
        __Marshaller_DailyWeatherModelRequest,
        __Marshaller_DailyWeatherModelResponse);

    /// <summary>Service descriptor</summary>
    public static global::Google.Protobuf.Reflection.ServiceDescriptor Descriptor
    {
      get { return global::DataCaptureService.WeatherReflection.Descriptor.Services[0]; }
    }

    /// <summary>Client for GrpcWeather</summary>
    public partial class GrpcWeatherClient : grpc::ClientBase<GrpcWeatherClient>
    {
      /// <summary>Creates a new client for GrpcWeather</summary>
      /// <param name="channel">The channel to use to make remote calls.</param>
      [global::System.CodeDom.Compiler.GeneratedCode("grpc_csharp_plugin", null)]
      public GrpcWeatherClient(grpc::ChannelBase channel) : base(channel)
      {
      }
      /// <summary>Creates a new client for GrpcWeather that uses a custom <c>CallInvoker</c>.</summary>
      /// <param name="callInvoker">The callInvoker to use to make remote calls.</param>
      [global::System.CodeDom.Compiler.GeneratedCode("grpc_csharp_plugin", null)]
      public GrpcWeatherClient(grpc::CallInvoker callInvoker) : base(callInvoker)
      {
      }
      /// <summary>Protected parameterless constructor to allow creation of test doubles.</summary>
      [global::System.CodeDom.Compiler.GeneratedCode("grpc_csharp_plugin", null)]
      protected GrpcWeatherClient() : base()
      {
      }
      /// <summary>Protected constructor to allow creation of configured clients.</summary>
      /// <param name="configuration">The client configuration.</param>
      [global::System.CodeDom.Compiler.GeneratedCode("grpc_csharp_plugin", null)]
      protected GrpcWeatherClient(ClientBaseConfiguration configuration) : base(configuration)
      {
      }

      [global::System.CodeDom.Compiler.GeneratedCode("grpc_csharp_plugin", null)]
      public virtual global::DataCaptureService.AirPollutionModelResponse AirPollution(global::DataCaptureService.AirPollutionModelRequest request, grpc::Metadata headers = null, global::System.DateTime? deadline = null, global::System.Threading.CancellationToken cancellationToken = default(global::System.Threading.CancellationToken))
      {
        return AirPollution(request, new grpc::CallOptions(headers, deadline, cancellationToken));
      }
      [global::System.CodeDom.Compiler.GeneratedCode("grpc_csharp_plugin", null)]
      public virtual global::DataCaptureService.AirPollutionModelResponse AirPollution(global::DataCaptureService.AirPollutionModelRequest request, grpc::CallOptions options)
      {
        return CallInvoker.BlockingUnaryCall(__Method_AirPollution, null, options, request);
      }
      [global::System.CodeDom.Compiler.GeneratedCode("grpc_csharp_plugin", null)]
      public virtual grpc::AsyncUnaryCall<global::DataCaptureService.AirPollutionModelResponse> AirPollutionAsync(global::DataCaptureService.AirPollutionModelRequest request, grpc::Metadata headers = null, global::System.DateTime? deadline = null, global::System.Threading.CancellationToken cancellationToken = default(global::System.Threading.CancellationToken))
      {
        return AirPollutionAsync(request, new grpc::CallOptions(headers, deadline, cancellationToken));
      }
      [global::System.CodeDom.Compiler.GeneratedCode("grpc_csharp_plugin", null)]
      public virtual grpc::AsyncUnaryCall<global::DataCaptureService.AirPollutionModelResponse> AirPollutionAsync(global::DataCaptureService.AirPollutionModelRequest request, grpc::CallOptions options)
      {
        return CallInvoker.AsyncUnaryCall(__Method_AirPollution, null, options, request);
      }
      [global::System.CodeDom.Compiler.GeneratedCode("grpc_csharp_plugin", null)]
      public virtual global::DataCaptureService.CurrentWeatherModelResponse CurrentWeather(global::DataCaptureService.CurrentWeatherModelRequest request, grpc::Metadata headers = null, global::System.DateTime? deadline = null, global::System.Threading.CancellationToken cancellationToken = default(global::System.Threading.CancellationToken))
      {
        return CurrentWeather(request, new grpc::CallOptions(headers, deadline, cancellationToken));
      }
      [global::System.CodeDom.Compiler.GeneratedCode("grpc_csharp_plugin", null)]
      public virtual global::DataCaptureService.CurrentWeatherModelResponse CurrentWeather(global::DataCaptureService.CurrentWeatherModelRequest request, grpc::CallOptions options)
      {
        return CallInvoker.BlockingUnaryCall(__Method_CurrentWeather, null, options, request);
      }
      [global::System.CodeDom.Compiler.GeneratedCode("grpc_csharp_plugin", null)]
      public virtual grpc::AsyncUnaryCall<global::DataCaptureService.CurrentWeatherModelResponse> CurrentWeatherAsync(global::DataCaptureService.CurrentWeatherModelRequest request, grpc::Metadata headers = null, global::System.DateTime? deadline = null, global::System.Threading.CancellationToken cancellationToken = default(global::System.Threading.CancellationToken))
      {
        return CurrentWeatherAsync(request, new grpc::CallOptions(headers, deadline, cancellationToken));
      }
      [global::System.CodeDom.Compiler.GeneratedCode("grpc_csharp_plugin", null)]
      public virtual grpc::AsyncUnaryCall<global::DataCaptureService.CurrentWeatherModelResponse> CurrentWeatherAsync(global::DataCaptureService.CurrentWeatherModelRequest request, grpc::CallOptions options)
      {
        return CallInvoker.AsyncUnaryCall(__Method_CurrentWeather, null, options, request);
      }
      [global::System.CodeDom.Compiler.GeneratedCode("grpc_csharp_plugin", null)]
      public virtual global::DataCaptureService.DailyWeatherModelResponse DailyWeather(global::DataCaptureService.DailyWeatherModelRequest request, grpc::Metadata headers = null, global::System.DateTime? deadline = null, global::System.Threading.CancellationToken cancellationToken = default(global::System.Threading.CancellationToken))
      {
        return DailyWeather(request, new grpc::CallOptions(headers, deadline, cancellationToken));
      }
      [global::System.CodeDom.Compiler.GeneratedCode("grpc_csharp_plugin", null)]
      public virtual global::DataCaptureService.DailyWeatherModelResponse DailyWeather(global::DataCaptureService.DailyWeatherModelRequest request, grpc::CallOptions options)
      {
        return CallInvoker.BlockingUnaryCall(__Method_DailyWeather, null, options, request);
      }
      [global::System.CodeDom.Compiler.GeneratedCode("grpc_csharp_plugin", null)]
      public virtual grpc::AsyncUnaryCall<global::DataCaptureService.DailyWeatherModelResponse> DailyWeatherAsync(global::DataCaptureService.DailyWeatherModelRequest request, grpc::Metadata headers = null, global::System.DateTime? deadline = null, global::System.Threading.CancellationToken cancellationToken = default(global::System.Threading.CancellationToken))
      {
        return DailyWeatherAsync(request, new grpc::CallOptions(headers, deadline, cancellationToken));
      }
      [global::System.CodeDom.Compiler.GeneratedCode("grpc_csharp_plugin", null)]
      public virtual grpc::AsyncUnaryCall<global::DataCaptureService.DailyWeatherModelResponse> DailyWeatherAsync(global::DataCaptureService.DailyWeatherModelRequest request, grpc::CallOptions options)
      {
        return CallInvoker.AsyncUnaryCall(__Method_DailyWeather, null, options, request);
      }
      /// <summary>Creates a new instance of client from given <c>ClientBaseConfiguration</c>.</summary>
      [global::System.CodeDom.Compiler.GeneratedCode("grpc_csharp_plugin", null)]
      protected override GrpcWeatherClient NewInstance(ClientBaseConfiguration configuration)
      {
        return new GrpcWeatherClient(configuration);
      }
    }

  }
}
#endregion