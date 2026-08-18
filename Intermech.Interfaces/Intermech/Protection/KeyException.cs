
// Type: Intermech.Protection.KeyException
// Assembly: Intermech.Interfaces, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0DE40E9E-DD84-4434-9A25-8F5A37D7D179

// XML documentation location: D:\IPS\Client\Intermech.Interfaces.xml

using Aladdin.HASP;
using Intermech.Localization;
using System;
using System.Runtime.Serialization;


namespace Intermech.Protection
{
    [Serializable]
    public class KeyException : Exception
    {
      private int _status;

      public KeyException(HaspStatus status)
        : this(status, string.Format(LocalizationHolder.rm.GetString("Interfaces_96"), (object) (int) status, (object) KeyException.StatusToString(status)))
      {
        this._status = (int) status;
      }

      private static string StatusToString(HaspStatus status)
      {
        string empty = string.Empty;
        string str;
        switch (status)
        {
          case HaspStatus.StatusOk:
            str = "Request successfully completed.";
            break;
          case HaspStatus.InvalidAddress:
            str = "Request exceeds memory range of a HASP file.";
            break;
          case HaspStatus.InvalidFeature:
            str = "Legacy HASP HL Run-time API: Unknown/Invalid Feature ID option.";
            break;
          case HaspStatus.NotEnoughMemory:
            str = "System is out of memory.";
            break;
          case HaspStatus.TooManyOpenFeatures:
            str = "Too many open Features/login sessions.";
            break;
          case HaspStatus.AccessDenied:
            str = "Access to Feature, HASP protection key or functionality denied.";
            break;
          case HaspStatus.IncompatibleFeature:
            str = "Legacy decryption function cannot work on Feature.";
            break;
          case HaspStatus.ContainerNotFound:
            str = "Sentinel HASP protection key not available.";
            break;
          case HaspStatus.BufferTooShort:
            str = "Encrypted/decrypted data length too short to execute function call.";
            break;
          case HaspStatus.InvalidHandle:
            str = "Invalid login handle passed to function.";
            break;
          case HaspStatus.InvalidFile:
            str = "Specified File ID not recognized by API.";
            break;
          case HaspStatus.DriverTooOld:
            str = "Installed driver or daemon too old to execute function.";
            break;
          case HaspStatus.NoTime:
            str = "Real-time clock (rtc) not available.";
            break;
          case HaspStatus.SystemError:
            str = "Generic error from host system call.";
            break;
          case HaspStatus.DriverNotFound:
            str = "Required driver not installed.";
            break;
          case HaspStatus.InvalidFormat:
            str = "Unrecognized file format for update.";
            break;
          case HaspStatus.RequestNotSupported:
            str = "Unable to execute function in this context.";
            break;
          case HaspStatus.InvalidUpdateObject:
            str = "Binary data passed to function does not contain valid update.";
            break;
          case HaspStatus.KeyIdNotFound:
            str = "HASP protection key not found.";
            break;
          case HaspStatus.InvalidUpdateData:
            str = "Required XML tags not found; Contents in binary data are missing.";
            break;
          case HaspStatus.UpdateNotSupported:
            str = "Update request not supported by Sentinel HASP protection key.";
            break;
          case HaspStatus.InvalidUpdateCounter:
            str = "Update counter set incorrectly.";
            break;
          case HaspStatus.InvalidVendorCode:
            str = "Invalid Vendor Code passed.";
            break;
          case HaspStatus.EncryptionNotSupported:
            str = "Sentinel HASP protection key does not support encryption type.";
            break;
          case HaspStatus.InvalidTime:
            str = "Passed time value outside supported value range.";
            break;
          case HaspStatus.NoBatteryPower:
            str = "Real-time clock battery out of power.";
            break;
          case HaspStatus.UpdateNoAckSpace:
            str = "Acknowledge data requested by update, but ack_data parameter is null.";
            break;
          case HaspStatus.TerminalServiceDetected:
            str = "Program running on a terminal server.";
            break;
          case HaspStatus.FeatureNotImplemented:
            str = "Requested Feature type not implemented.";
            break;
          case HaspStatus.UnknownAlgorithm:
            str = "Unknown algorithm used in H2R/V2C file.";
            break;
          case HaspStatus.InvalidSignature:
            str = "Signature verification operation failed.";
            break;
          case HaspStatus.FeatureNotFound:
            str = "Requested Feature not available.";
            break;
          case HaspStatus.NoLog:
            str = "Access log not enabled.";
            break;
          case HaspStatus.LocalCommErr:
            str = "Communication error between API and local HASP License Manager.";
            break;
          case HaspStatus.UnknownVcode:
            str = "Vendor Code not recognized by API.";
            break;
          case HaspStatus.InvalidXmlSpec:
            str = "Invalid XML specification.";
            break;
          case HaspStatus.InvalidXmlScope:
            str = "Invalid XML scope.";
            break;
          case HaspStatus.TooManyKeys:
            str = "Too many Sentinel HASP protection keys currently connected.";
            break;
          case HaspStatus.TooManyUsers:
            str = "Too many concurrent user sessions currently connected.";
            break;
          case HaspStatus.BrokenSession:
            str = "Session been interrupted.";
            break;
          case HaspStatus.RemoteCommErr:
            str = "Communication error between local and remote HASP License Managers.";
            break;
          case HaspStatus.FeatureExpired:
            str = "Feature expired.";
            break;
          case HaspStatus.TooOldLM:
            str = "HASP License Manager version too old.";
            break;
          case HaspStatus.DeviceError:
            str = "Input/Output error occurred.";
            break;
          case HaspStatus.UpdateBlocked:
            str = "Update installation not permitted; This update was already applied.";
            break;
          case HaspStatus.TimeError:
            str = "System time has been tampered with.";
            break;
          case HaspStatus.SecureChannelError:
            str = "Communication error occurred in secure channel.";
            break;
          case HaspStatus.CorruptStorage:
            str = "Corrupt data exists in secure storage area of HASP SL protection key.";
            break;
          case HaspStatus.VendorLibNotFound:
            str = "Unable to find Vendor library.";
            break;
          case HaspStatus.InvalidVendorLib:
            str = "Unable to load Vendor library.";
            break;
          case HaspStatus.EmptyScopeResults:
            str = "Unable to locate any Feature matching scope.";
            break;
          case HaspStatus.VMDetected:
            str = "Program running on a virtual machine.";
            break;
          case HaspStatus.HardwareModified:
            str = "HASP SL key incompatible.";
            break;
          case HaspStatus.UserDenied:
            str = "Login denied because of user restrictions.";
            break;
          case HaspStatus.UpdateTooOld:
            str = "Update to old.";
            break;
          case HaspStatus.UpdateTooNew:
            str = "Update to new.";
            break;
          case HaspStatus.VendorlibOld:
            str = "Old vendor lib.";
            break;
          case HaspStatus.UploadError:
            str = "Upload via ACC failed, e.g. because of illegal format.";
            break;
          case HaspStatus.InvalidRecipient:
            str = "Invalid XML \"recipient\" parameter.";
            break;
          case HaspStatus.InvalidDetachAction:
            str = "Invalid XML \"action\" parameter.";
            break;
          case HaspStatus.TooManyProducts:
            str = "scope does not specify a unique Product.";
            break;
          case HaspStatus.InvalidProduct:
            str = "Invalid Product information.";
            break;
          case HaspStatus.UnknownRecipient:
            str = "Unknown Recipient.";
            break;
          case HaspStatus.InvalidDuration:
            str = "Invalid Duration.";
            break;
          case HaspStatus.CloneDetected:
            str = "Cloned HASP SL secure storage detected.";
            break;
          case HaspStatus.UpdateAlreadyAdded:
            str = "Specified v2c update already installed in the LLM.";
            break;
          case HaspStatus.HaspInactive:
            str = "Specified Hasp Id is in Inactive state.";
            break;
          case HaspStatus.NoDetachableFeature:
            str = "No detachable feature exists.";
            break;
          case HaspStatus.TooManyHosts:
            str = "scope does not specify a unique Host.";
            break;
          case HaspStatus.RehostNotAllowed:
            str = "Rehost is not allowed for any license.";
            break;
          case HaspStatus.LicenseRehosted:
            str = "License is rehosted to other machine.";
            break;
          case HaspStatus.RehostAlreadyApplied:
            str = "Old rehost license try to apply.";
            break;
          case HaspStatus.CannotReadFile:
            str = "File not found or access denied.";
            break;
          case HaspStatus.NoApiDylib:
            str = "API dispatcher: API for this Vendor Code was not found.";
            break;
          case HaspStatus.InvApiDylib:
            str = "API dispatcher: Unable to load API; DLL possibly corrupt?.";
            break;
          case HaspStatus.InvalidObject:
            str = "C++ API: Object incorrectly initialized.";
            break;
          case HaspStatus.InvalidParameter:
            str = "C++ API: Invalid function parameter.";
            break;
          case HaspStatus.AlreadyLoggedIn:
            str = "C++ API: Logging in twice to the same object.";
            break;
          case HaspStatus.AlreadyLoggedOut:
            str = "C++ API: Logging out twice of the same object.";
            break;
          case HaspStatus.OperationFailed:
            str = ".NET API: Incorrect use of system or platform.";
            break;
          case HaspStatus.NoExtensionBlock:
            str = "Internal use: no classic memory extension block available.";
            break;
          case HaspStatus.InvalidPortType:
            str = "Internal use: invalid port type.";
            break;
          case HaspStatus.InvalidPort:
            str = "Internal use: invalid port value.";
            break;
          case HaspStatus.NotImplemented:
            str = "Requested function not implemented.";
            break;
          case HaspStatus.InternalError:
            str = "Internal error occurred in API.";
            break;
          default:
            str = status.ToString();
            break;
        }
        return $"[{status.ToString()}] {str}";
      }

      public KeyException(HaspStatus status, string message)
        : base(message)
      {
        this._status = (int) status;
      }

      protected KeyException(SerializationInfo info, StreamingContext context)
        : base(info, context)
      {
        this._status = info.GetInt32("status");
      }

      public int Status => this._status;

      public override void GetObjectData(SerializationInfo info, StreamingContext context)
      {
        base.GetObjectData(info, context);
        info.AddValue("status", this._status);
      }
    }
}
