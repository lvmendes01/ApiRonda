# Configurações
$projectFile = "D:\Empresas\LVMendes\RondaSegurancaBack\Ronda\apiRonda\RondaSegurancaBack.csproj"
$publishFolder = "D:\Empresas\LVMendes\RondaSegurancaBack\Ronda\apiRonda\publish"
$remoteUser = "root"
$remoteHost = "72.62.137.230"
$remoteAppPath = "/var/www/lvmendes/app"   # <-- pasta da aplicação
$serviceName = "ronda"

# 0️⃣ Limpar pasta local de publish
if (Test-Path $publishFolder) {
    Write-Host "Limpando pasta de publish local..."
    Remove-Item "$publishFolder\*" -Recurse -Force
} else {
    Write-Host "Pasta de publish não existe. Criando..."
    New-Item -ItemType Directory -Path $publishFolder
}

# 1️⃣ Publicar a aplicação
Write-Host "Publicando a aplicação..."
dotnet publish "$projectFile" -c Release -o "$publishFolder"

# 2️⃣ Parar serviço no servidor
Write-Host "Parando serviço $serviceName..."
ssh "$remoteUser@$remoteHost" "sudo systemctl stop $serviceName"

# 3️⃣ Limpar apenas a pasta da aplicação (sem tocar uploads)
Write-Host "Limpando pasta da aplicação $remoteAppPath..."
ssh "$remoteUser@$remoteHost" "sudo rm -rf ${remoteAppPath}/*"

# 4️⃣ Enviar arquivos novos
Write-Host "Enviando arquivos para $remoteHost..."
scp -r "$publishFolder\*" "${remoteUser}@${remoteHost}:${remoteAppPath}/"

# 5️⃣ Reiniciar serviço
Write-Host "Iniciando serviço $serviceName..."
ssh "$remoteUser@$remoteHost" "sudo systemctl daemon-reload; sudo systemctl start $serviceName; sudo systemctl enable $serviceName; sudo systemctl status $serviceName"

Write-Host "✅ Deploy concluído com sucesso!"
