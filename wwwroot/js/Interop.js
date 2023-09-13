export function getMessage() {
    return 'Olá do Blazor!';
}

export async function setMessage() {
    const { getAssemblyExports } = await globalThis.getDotnetRuntime(0);
    var exports = await getAssemblyExports("BlazorSample.dll");

    document.getElementById("result").innerText =
        exports.BlazorSample.JavaScriptInterop.Interop.GetMessageFromDotnet();
}