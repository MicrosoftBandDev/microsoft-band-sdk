// Decompiled with JetBrains decompiler
// Type: Microsoft.Band.BandControllerSubscriptions
// Assembly: Microsoft.Band.Store_UAP, Version=1.3.20628.2, Culture=neutral, PublicKeyToken=608d7da3159f502b
// MVID: 018DB8C7-131F-4953-86CB-5B9FE6B445B8
// Assembly location: C:\Users\Claudio\AppData\Local\Temp\Pofaker\c569910e50\lib\uap10.0\Microsoft.Band.Store_UAP.dll

using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using Windows.ApplicationModel;
using Windows.ApplicationModel.AppService;
using Windows.Foundation;
using Windows.Foundation.Collections;

namespace Microsoft.Band
{
  internal class BandControllerSubscriptions
  {
    internal static async Task<AppServiceResponse> SendRequestToBandControllerServiceAsync(
      ValueSet requestMessage)
    {
      string forAppServiceAsync = await BandControllerSubscriptions.GetPackageFamilyNameForAppServiceAsync(InternalBandControllerNames.BandControllerServiceName);
      AppServiceResponse appServiceResponse;
      using (AppServiceConnection bandControllerService = new AppServiceConnection())
      {
        bandControllerService.put_AppServiceName(InternalBandControllerNames.BandControllerServiceName);
        bandControllerService.put_PackageFamilyName(forAppServiceAsync);
        TaskAwaiter<AppServiceConnectionStatus> awaiter1 = (TaskAwaiter<AppServiceConnectionStatus>) WindowsRuntimeSystemExtensions.GetAwaiter<AppServiceConnectionStatus>((IAsyncOperation<M0>) bandControllerService.OpenAsync());
        int num;
        if (!awaiter1.IsCompleted)
        {
          // ISSUE: explicit reference operation
          // ISSUE: reference to a compiler-generated field
          (^this).\u003C\u003E1__state = num = 1;
          TaskAwaiter<AppServiceConnectionStatus> taskAwaiter = awaiter1;
          // ISSUE: explicit reference operation
          // ISSUE: reference to a compiler-generated field
          (^this).\u003C\u003Et__builder.AwaitUnsafeOnCompleted<TaskAwaiter<AppServiceConnectionStatus>, BandControllerSubscriptions.\u003CSendRequestToBandControllerServiceAsync\u003Ed__0>(ref awaiter1, this);
          return;
        }
        AppServiceConnectionStatus result1 = awaiter1.GetResult();
        awaiter1 = new TaskAwaiter<AppServiceConnectionStatus>();
        AppServiceConnectionStatus connectionStatus = result1;
        if (connectionStatus != null)
          throw new BandException(string.Format("Unable to connect to service, error {0}", (object) connectionStatus.ToString()));
        TaskAwaiter<AppServiceResponse> awaiter2 = (TaskAwaiter<AppServiceResponse>) WindowsRuntimeSystemExtensions.GetAwaiter<AppServiceResponse>((IAsyncOperation<M0>) bandControllerService.SendMessageAsync(requestMessage));
        if (!awaiter2.IsCompleted)
        {
          // ISSUE: explicit reference operation
          // ISSUE: reference to a compiler-generated field
          (^this).\u003C\u003E1__state = num = 2;
          TaskAwaiter<AppServiceResponse> taskAwaiter = awaiter2;
          // ISSUE: explicit reference operation
          // ISSUE: reference to a compiler-generated field
          (^this).\u003C\u003Et__builder.AwaitUnsafeOnCompleted<TaskAwaiter<AppServiceResponse>, BandControllerSubscriptions.\u003CSendRequestToBandControllerServiceAsync\u003Ed__0>(ref awaiter2, this);
          return;
        }
        AppServiceResponse result2 = awaiter2.GetResult();
        awaiter2 = new TaskAwaiter<AppServiceResponse>();
        appServiceResponse = result2;
      }
      return appServiceResponse;
    }

    internal static void CheckResponse(AppServiceResponse response)
    {
      if (response.Status != null)
        throw new BandException(string.Format("Response error {0}", (object) response.Status));
      bool? nullable = ((IDictionary<string, object>) response.Message)[InternalBandControllerNames.ResponseSucceededParameterName] as bool?;
      if (!nullable.HasValue)
        throw new BandException("Response missing 'Succeeded'");
      if (nullable.Value)
        return;
      if (!(((IDictionary<string, object>) response.Message)[InternalBandControllerNames.ResponseStatusParameterName] is string str))
        throw new BandException("Response missing 'Status'");
      throw new BandException(string.Format("Response Status {0}", (object) str));
    }

    private static async Task<string> GetPackageFamilyNameForAppServiceAsync(
      string appServiceName)
    {
      TaskAwaiter<IReadOnlyList<AppInfo>> awaiter = (TaskAwaiter<IReadOnlyList<AppInfo>>) WindowsRuntimeSystemExtensions.GetAwaiter<IReadOnlyList<AppInfo>>((IAsyncOperation<M0>) AppServiceCatalog.FindAppServiceProvidersAsync(appServiceName));
      if (!awaiter.IsCompleted)
      {
        int num;
        // ISSUE: explicit reference operation
        // ISSUE: reference to a compiler-generated field
        (^this).\u003C\u003E1__state = num = 0;
        TaskAwaiter<IReadOnlyList<AppInfo>> taskAwaiter = awaiter;
        // ISSUE: explicit reference operation
        // ISSUE: reference to a compiler-generated field
        (^this).\u003C\u003Et__builder.AwaitUnsafeOnCompleted<TaskAwaiter<IReadOnlyList<AppInfo>>, BandControllerSubscriptions.\u003CGetPackageFamilyNameForAppServiceAsync\u003Ed__2>(ref awaiter, this);
      }
      else
      {
        IReadOnlyList<AppInfo> result = awaiter.GetResult();
        awaiter = new TaskAwaiter<IReadOnlyList<AppInfo>>();
        IReadOnlyList<AppInfo> appInfoList = result;
        if (appInfoList == null || ((IReadOnlyCollection<AppInfo>) appInfoList).Count == 0)
          throw new BandException(string.Format("No service installed for AppService {0}", (object) appServiceName));
        return Enumerable.Count<AppInfo>((IEnumerable<M0>) appInfoList) <= 1 ? appInfoList[0].PackageFamilyName : throw new BandException(string.Format("Multiple services installed for AppService {0}", (object) appServiceName));
      }
    }
  }
}
