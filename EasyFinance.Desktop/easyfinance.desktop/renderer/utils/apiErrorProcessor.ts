interface ErrorData {
    type?: string;
    title?: string;
    status?: number;
    traceId?: string;
    errors?: { [key: string]: string[] };
  }

  class ApiErrorProcessor {
    static processError(error: any) {
      console.log(error.response.data);

      const errorData: ErrorData = error.response.data;

      console.log(errorData);

      let errorMessage = '';
      if (errorData.errors && Object.keys(errorData.errors).length > 0) {
        const errorMessages = Object.entries(errorData.errors).map(([key, values]) => {
          const processedErrors = values.map((error: string) => 
            `<li>* ${ApiErrorProcessor.capitalizeFirstLetter(error.trim())}</li>`
          ).join('');
          return `<li>${ApiErrorProcessor.capitalizeFirstLetter(key)}:<ul>${processedErrors}</ul></li>`;
        }).join('');
        errorMessage = `<ul>${errorMessages}</ul>`;
      } else if (errorData.title) {
        errorMessage = ApiErrorProcessor.capitalizeFirstLetter(errorData.title);
      } else {
        errorMessage = 'Erro ocorrido no servidor. Tente novamente mais tarde.';
      }
  
      return errorMessage;
    }
  
    private static capitalizeFirstLetter(string: string) {
      return string.charAt(0).toUpperCase() + string.slice(1).toLowerCase();
    }
  }
  
  export default ApiErrorProcessor;