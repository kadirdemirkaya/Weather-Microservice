// <auto-generated>
//     Generated by the protocol buffer compiler.  DO NOT EDIT!
//     source: Protos/location.proto
// </auto-generated>
#pragma warning disable 0414, 1591, 8981, 0612
#region Designer generated code

using grpc = global::Grpc.Core;

namespace LocationService {
  public static partial class GrpcLocation
  {
    static readonly string __ServiceName = "GrpcLocation";

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
    static readonly grpc::Marshaller<global::LocationService.AirPollutionModelRequest> __Marshaller_AirPollutionModelRequest = grpc::Marshallers.Create(__Helper_SerializeMessage, context => __Helper_DeserializeMessage(context, global::LocationService.AirPollutionModelRequest.Parser));
    [global::System.CodeDom.Compiler.GeneratedCode("grpc_csharp_plugin", null)]
    static readonly grpc::Marshaller<global::LocationService.AirPollutionModelResponse> __Marshaller_AirPollutionModelResponse = grpc::Marshallers.Create(__Helper_SerializeMessage, context => __Helper_DeserializeMessage(context, global::LocationService.AirPollutionModelResponse.Parser));
    [global::System.CodeDom.Compiler.GeneratedCode("grpc_csharp_plugin", null)]
    static readonly grpc::Marshaller<global::LocationService.CurrentWeatherModelRequest> __Marshaller_CurrentWeatherModelRequest = grpc::Marshallers.Create(__Helper_SerializeMessage, context => __Helper_DeserializeMessage(context, global::LocationService.CurrentWeatherModelRequest.Parser));
    [global::System.CodeDom.Compiler.GeneratedCode("grpc_csharp_plugin", null)]
    static readonly grpc::Marshaller<global::LocationService.CurrentWeatherModelResponse> __Marshaller_CurrentWeatherModelResponse = grpc::Marshallers.Create(__Helper_SerializeMessage, context => __Helper_DeserializeMessage(context, global::LocationService.CurrentWeatherModelResponse.Parser));
    [global::System.CodeDom.Compiler.GeneratedCode("grpc_csharp_plugin", null)]
    static readonly grpc::Marshaller<global::LocationService.DailyWeatherModelRequest> __Marshaller_DailyWeatherModelRequest = grpc::Marshallers.Create(__Helper_SerializeMessage, context => __Helper_DeserializeMessage(context, global::LocationService.DailyWeatherModelRequest.Parser));
    [global::System.CodeDom.Compiler.GeneratedCode("grpc_csharp_plugin", null)]
    static readonly grpc::Marshaller<global::LocationService.DailyWeatherModelResponse> __Marshaller_DailyWeatherModelResponse = grpc::Marshallers.Create(__Helper_SerializeMessage, context => __Helper_DeserializeMessage(context, global::LocationService.DailyWeatherModelResponse.Parser));

    [global::System.CodeDom.Compiler.GeneratedCode("grpc_csharp_plugin", null)]
    static readonly grpc::Method<global::LocationService.AirPollutionModelRequest, global::LocationService.AirPollutionModelResponse> __Method_AirPollution = new grpc::Method<global::LocationService.AirPollutionModelRequest, global::LocationService.AirPollutionModelResponse>(
        grpc::MethodType.Unary,
        __ServiceName,
        "AirPollution",
        __Marshaller_AirPollutionModelRequest,
        __Marshaller_AirPollutionModelResponse);

    [global::System.CodeDom.Compiler.GeneratedCode("grpc_csharp_plugin", null)]
    static readonly grpc::Method<global::LocationService.CurrentWeatherModelRequest, global::LocationService.CurrentWeatherModelResponse> __Method_CurrentWeather = new grpc::Method<global::LocationService.CurrentWeatherModelRequest, global::LocationService.CurrentWeatherModelResponse>(
        grpc::MethodType.Unary,
        __ServiceName,
        "CurrentWeather",
        __Marshaller_CurrentWeatherModelRequest,
        __Marshaller_CurrentWeatherModelResponse);

    [global::System.CodeDom.Compiler.GeneratedCode("grpc_csharp_plugin", null)]
    static readonly grpc::Method<global::LocationService.DailyWeatherModelRequest, global::LocationService.DailyWeatherModelResponse> __Method_DailyWeather = new grpc::Method<global::LocationService.DailyWeatherModelRequest, global::LocationService.DailyWeatherModelResponse>(
        grpc::MethodType.Unary,
        __ServiceName,
        "DailyWeather",
        __Marshaller_DailyWeatherModelRequest,
        __Marshaller_DailyWeatherModelResponse);

    /// <summary>Service descriptor</summary>
    public static global::Google.Protobuf.Reflection.ServiceDescriptor Descriptor
    {
      get { return global::LocationService.LocationReflection.Descriptor.Services[0]; }
    }

    /// <summary>Base class for server-side implementations of GrpcLocation</summary>
    [grpc::BindServiceMethod(typeof(GrpcLocation), "BindService")]
    public abstract partial class GrpcLocationBase
    {
      [global::System.CodeDom.Compiler.GeneratedCode("grpc_csharp_plugin", null)]
      public virtual global::System.Threading.Tasks.Task<global::LocationService.AirPollutionModelResponse> AirPollution(global::LocationService.AirPollutionModelRequest request, grpc::ServerCallContext context)
      {
        throw new grpc::RpcException(new grpc::Status(grpc::StatusCode.Unimplemented, ""));
      }

      [global::System.CodeDom.Compiler.GeneratedCode("grpc_csharp_plugin", null)]
      public virtual global::System.Threading.Tasks.Task<global::LocationService.CurrentWeatherModelResponse> CurrentWeather(global::LocationService.CurrentWeatherModelRequest request, grpc::ServerCallContext context)
      {
        throw new grpc::RpcException(new grpc::Status(grpc::StatusCode.Unimplemented, ""));
      }

      [global::System.CodeDom.Compiler.GeneratedCode("grpc_csharp_plugin", null)]
      public virtual global::System.Threading.Tasks.Task<global::LocationService.DailyWeatherModelResponse> DailyWeather(global::LocationService.DailyWeatherModelRequest request, grpc::ServerCallContext context)
      {
        throw new grpc::RpcException(new grpc::Status(grpc::StatusCode.Unimplemented, ""));
      }

    }

    /// <summary>Creates service definition that can be registered with a server</summary>
    /// <param name="serviceImpl">An object implementing the server-side handling logic.</param>
    [global::System.CodeDom.Compiler.GeneratedCode("grpc_csharp_plugin", null)]
    public static grpc::ServerServiceDefinition BindService(GrpcLocationBase serviceImpl)
    {
      return grpc::ServerServiceDefinition.CreateBuilder()
          .AddMethod(__Method_AirPollution, serviceImpl.AirPollution)
          .AddMethod(__Method_CurrentWeather, serviceImpl.CurrentWeather)
          .AddMethod(__Method_DailyWeather, serviceImpl.DailyWeather).Build();
    }

    /// <summary>Register service method with a service binder with or without implementation. Useful when customizing the service binding logic.
    /// Note: this method is part of an experimental API that can change or be removed without any prior notice.</summary>
    /// <param name="serviceBinder">Service methods will be bound by calling <c>AddMethod</c> on this object.</param>
    /// <param name="serviceImpl">An object implementing the server-side handling logic.</param>
    [global::System.CodeDom.Compiler.GeneratedCode("grpc_csharp_plugin", null)]
    public static void BindService(grpc::ServiceBinderBase serviceBinder, GrpcLocationBase serviceImpl)
    {
      serviceBinder.AddMethod(__Method_AirPollution, serviceImpl == null ? null : new grpc::UnaryServerMethod<global::LocationService.AirPollutionModelRequest, global::LocationService.AirPollutionModelResponse>(serviceImpl.AirPollution));
      serviceBinder.AddMethod(__Method_CurrentWeather, serviceImpl == null ? null : new grpc::UnaryServerMethod<global::LocationService.CurrentWeatherModelRequest, global::LocationService.CurrentWeatherModelResponse>(serviceImpl.CurrentWeather));
      serviceBinder.AddMethod(__Method_DailyWeather, serviceImpl == null ? null : new grpc::UnaryServerMethod<global::LocationService.DailyWeatherModelRequest, global::LocationService.DailyWeatherModelResponse>(serviceImpl.DailyWeather));
    }

  }
}
#endregion