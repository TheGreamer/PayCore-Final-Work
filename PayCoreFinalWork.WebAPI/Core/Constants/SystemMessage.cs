namespace PayCoreFinalWork.Core.Constants
{
    // Sistemde yer alan tüm mesajların tek çatı altında toplandığı yapı.
    public struct SystemMessage
    {
        public static readonly string API_ID_NULL = "Id can't be empty.";
        public static readonly string API_NOT_EMPTY = "Must not be empty.";
        public static readonly string API_NOT_FOUND = "No item found.";
        public static readonly string API_ADDED = "Added successfully.";
        public static readonly string API_UPDATED = "Updated successfully.";
        public static readonly string API_UPDATE_ERROR = "Error occured while updating.";
        public static readonly string API_DELETED = "Deleted successfully.";
        public static readonly string API_DELETE_ERROR = "The object that you are trying to delete has more than 0 items. You must delete those items first.";
        public static readonly string API_SAME_DATA_ERROR = "This data has already been added.";

        public static readonly string USER_FIRST_NAME_EMPTY_ERROR = "First name cannot be empty.";
        public static readonly string USER_LAST_NAME_EMPTY_ERROR = "Last name cannot be empty.";
        public static readonly string USER_NAME_EMPTY_ERROR = "User name cannot be empty.";
        public static readonly string USER_EMAIL_ERROR = "A valid email address is required.";
        public static readonly string USER_PASSWORD_EMPTY_ERROR = "Password cannot be empty.";
        public static readonly string USER_PHONE_EMPTY_ERROR = "Phone number cannot be empty.";
        public static readonly string USER_PASSWORD_LENGTH_ERROR = "Password length must be between 8 and 20 characters.";
        public static readonly string USER_FIRST_NAME_LENGTH_ERROR = "First name's length must be less than or equal to 40.";
        public static readonly string USER_LAST_NAME_LENGTH_ERROR = "Last name's length must be less than or equal to 40.";
        public static readonly string USER_NAME_LENGTH_ERROR = "User name's length must be less than or equal to 25.";
        public static readonly string USER_PHONE_REGEX_ERROR = "A valid phone number is required. (Example: +90 212 123 12 12)";
        public static readonly string USER_PASSWORD_NUMBER_ERROR = "Password must contain at least 1 number.";
        public static readonly string USER_PASSWORD_UPPER_CASE_ERROR = "Password must contain at least 1 upper case letter.";
        public static readonly string USER_PASSWORD_LOWER_CASE_ERROR = "Password must contain at least 1 lower case letter.";
        public static readonly string USER_REGISTERED = "Successfully registered.";
        public static readonly string USER_EMAIL_EXISTS = "This email is already in use.";
        public static readonly string USER_NAME_EXISTS = "This user name is already in use.";

        public static readonly string CATEGORY_NAME_EMPTY_ERROR = "Category name cannot be empty.";
        public static readonly string CATEGORY_NAME_LENGTH_ERROR = "Category name's length must be less than or equal to 50.";
        public static readonly string CATEGORY_DESCRIPTION_LENGTH_ERROR = "Category description's length must be less than or equal to 1000.";

        public static readonly string PRODUCT_NAME_EMPTY_ERROR = "Product name cannot be empty.";
        public static readonly string PRODUCT_COLOR_EMPTY_ERROR = "Product color cannot be empty.";
        public static readonly string PRODUCT_BRAND_EMPTY_ERROR = "Product brand cannot be empty.";
        public static readonly string PRODUCT_PRICE_EMPTY_ERROR = "Product price cannot be empty.";
        public static readonly string PRODUCT_CATEGORYID_EMPTY_ERROR = "Product's category id cannot be empty.";
        public static readonly string PRODUCT_NAME_LENGTH_ERROR = "Product name's length must be less than or equal to 100.";
        public static readonly string PRODUCT_DESCRIPTION_EMPTY_ERROR = "Product description cannot be empty.";
        public static readonly string PRODUCT_DESCRIPTION_LENGTH_ERROR = "Product description's length must be less than or equal to 500.";
        public static readonly string PRODUCT_IS_OFFERABLE_EMPTY_ERROR = "Product's offer state cannot be empty.";
        public static readonly string PRODUCT_IS_ANOTHER_USERS_ERROR = "This product is another user's product.";
        public static readonly string PRODUCT_BUY_SUCCESS = "Product has been successfully bought.";
        public static readonly string PRODUCT_BUY_FAILURE = "An error occured while processing purchase operation.";
        public static readonly string PRODUCT_IS_SOLD = "This product has been already sold.";

        public static readonly string OFFER_PRODUCT_EMPTY_ERROR = "Offer's product id cannot be empty.";
        public static readonly string OFFER_PRICE_EMPTY_ERROR = "Offer's price cannot be empty.";
        public static readonly string OFFER_PRICE_RANGE_ERROR = "Offer's price must be between 1 and 50000.";
        public static readonly string OFFER_SUCCESSFULL = "Offer has been successfully sent.";
        public static readonly string OFFER_EXISTS_ERROR = "This offer was made.";
        public static readonly string OFFER_NOT_EXISTS_ERROR = "This offer does not exist.";
        public static readonly string OFFER_IS_NOT_OFFERABLE = "Requested product is not offerable.";
        public static readonly string OFFER_IS_ANOTHER_USERS_ERROR = "This offer is another user's offer.";
        public static readonly string OFFER_ACCEPTED = "Offer accepted.";
        public static readonly string OFFER_DENIED = "Offer denied.";
        public static readonly string OFFER_ANSWER_FAILURE = "An error occured while processing offer answer operation.";

        public static readonly string TOKEN_NULL_ERROR = "Valid informations must be entered.";
        public static readonly string TOKEN_INFO_ERROR = "Provided informations must be validated.";
        public static readonly string TOKEN_GENERATION_ERROR = "Token generation error.";
        public static readonly string TOKEN_LAST_ACTIVITY_ERROR = "An error occurred while updating last activity for user";

        public static readonly string EMAIL_SEND_ERROR = "Email could not sent.";
    }
}