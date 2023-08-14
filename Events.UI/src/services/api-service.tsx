export const FetchApiPromise = async (
  url: string,
  method: string,
  jsonData: any = {},
  apiHeaders: any = {
    "content-type": "application/json",
    "Access-Control-Allow-Origin": "*",
    "Access-Control-Allow-Methods": "GET,POST,PATCH,OPTIONS",
  }
) => {
  let apiResponse = null;
  try {
    switch (method) {
      case "GET": {
        apiResponse = FetchGet(url, apiHeaders);
        break;
      }
      case "POST": {
        apiResponse = FetchPost(url, jsonData, apiHeaders);
        break;
      }
      case "PUT": {
        apiResponse = FetchPut(url, jsonData, apiHeaders);
        break;
      }
      case "DELETE": {
        apiResponse = FetchDelete(url, jsonData, apiHeaders);
        break;
      }
    }
  } catch (err) {
    console.log(err);
  }
  return apiResponse;
};

export const FetchPost = (url: string, jsonData: string, apiHeaders: any) => {
  return fetch(url, {
    method: "POST",
    headers: apiHeaders,
    body: jsonData,
  });
};

export const FetchGet = (url: string, apiHeaders: any) => {
  return fetch(url, { headers: apiHeaders });
};

export const FetchPut = (url: string, jsonData: string, apiHeaders: any) => {
  return fetch(url, {
    method: "PUT",
    headers: apiHeaders,
    body: jsonData,
  });
};

export const FetchDelete = (url: string, jsonData: string, apiHeaders: any) => {
  return fetch(url, {
    method: "DELETE",
    headers: apiHeaders,
    body: JSON.stringify(jsonData),
  });
};
