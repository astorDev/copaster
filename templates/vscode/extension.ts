import * as vscode from 'vscode';

export function activate(context: vscode.ExtensionContext) {
    const output = vscode.window.createOutputChannel('Template');
    const version = context.extension.packageJSON.version;
    output.appendLine(`Template Starting. Version: ${version}`);
    output.show();
}

export function deactivate() {}
