﻿namespace WSPay.Net
{
    public class ApiResponse
    {
        private ApiResponse(bool isSuccess, string resultContent = null, string errorMessage = null)
        {
            this.IsSuccess = isSuccess;
            this.ResultContent = resultContent;
            this.ErrorMessage = errorMessage;
        }

        public static ApiResponse CreateError(string errorMessage = null)
        {
            return new ApiResponse(false, null, errorMessage);
        }

        public static ApiResponse CreateSuccess(string resultContent)
        {
            return new ApiResponse(true, resultContent);
        }

        public bool IsSuccess { get; private set; }
        public string ErrorMessage { get; private set; }
        public string ResultContent { get; private set; }
    }
}
