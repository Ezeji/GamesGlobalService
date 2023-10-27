﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GamesGlobalCore.Constants
{
    public static class ServiceCodes
    {
        public const string Success = "00";
        public const string BadRequest = "89";
        public const string Forbidden = "98";
        public const string Unauthorized = "95";
        public const string ServerError = "99";
    }

    public static class ServiceMessages
    {
        public const string Success = "The operation was successful";
        public const string Failed = "An unhandled error has occurred while processing your request";
        public const string UpdateError = "There was an error carrying out operation";
        public const string MisMatch = "The entity Id does not match the supplied Id";
        public const string EntityIsNull = "Supplied entity is null or supplied list of entities is empty. Check our docs";
        public const string EntityNotFound = "The requested resource was not found. Verify that the supplied Id is correct";
        public const string Incompleted = "Some actions may not have been successfully processed";
        public const string EntityExist = "An entity of the same name or id exists";
        public const string InvalidParam = "A supplied parameter or model is invalid. Check the docs";
        public const string UnprocessableEntity = "The action cannot be processed";
        public const string InternalServerError = "An internal server error and request could not processed";
        public const string OperationFailed = "Operation failed";
        public const string ParameterEmptyOrNull = "The parameter list is null or empty";
        public const string RequestIdRequired = "Request Id is required";
    }

    public static class FibonacciServiceConstants
    {
        public const string SubsequenceFirstNumberFailed = "Generating subsequence first number failed as the expiry duration has elapsed";
        public const string TimeoutOccurred = "A timeout has occurred while generating the subsequence numbers";
        public const string SubsequenceLastNumberIndexExist = "Subsequence last number index exist as the recursive base case";
        public const string SubsequenceNumbersCreationCompleted = "Generating the subsequence numbers was successfully completed within the expiry duration";
        public const string AllocatedMemoryReached = "The allocated memory for the subsequence numbers has been exhausted";
    }
}
