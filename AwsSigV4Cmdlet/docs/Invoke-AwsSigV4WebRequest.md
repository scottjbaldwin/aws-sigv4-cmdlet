---
external help file: AwsSigV4.dll-Help.xml
Module Name: AwsSigV4
online version:
schema: 2.0.0
---

# Invoke-AwsSigV4WebRequest

## SYNOPSIS
Web Requests using AWS SigV4.

## SYNTAX

```
Invoke-AwsSigV4WebRequest [-Uri] <Uri> [-Body <String>] -AccessKey <String> -SecretKey <String>
 [-Token <String>] [-Method <String>] -Region <String> [-Service <String>] [-ContentType <String>]
 [<CommonParameters>]
```

## DESCRIPTION
 Makes a signed web request to a uri with AWS Signature Version 4 signing process for APIs that use AWS IAM authentication.
This command is designed to be as close as is possible to being a drop-in replacement for the powershell Invoke-WebRequest
but there are points where we have had to depart from Invoke-WebRequest for various reasons.

## EXAMPLES

### Example 1
```powershell
PS C:\> Invoke-AwsSigV4WebRequest -Uri https://foo.com/api/values -AccessKey AKIAXXXXXXXXXXXX -SecretKey XXXXXXXXXXXXXXXXXXXXXXXXXXXXX -Region us-east-1
```

Make a GET request on the foo.com api to get the values

### Example 2
```powershell
PS C:\> Get-Content .\requestbody.json | Invoke-AwsSigV4WebRequest -Uri https://foo.com/api/values -Method POST -AccessKey AKIAXXXXXXXXXXXX -SecretKey XXXXXXXXXXXXXXXXXXXXXXXXXXXXX -Region us-east-1
```

Uses the contents of the file requestbody.json as the Body of the POST request to the foo.com api

## PARAMETERS

### -AccessKey
The access key of the IAM principal.

```yaml
Type: String
Parameter Sets: (All)
Aliases:

Required: True
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -Body
The Body of the request.

```yaml
Type: String
Parameter Sets: (All)
Aliases:

Required: False
Position: Named
Default value: None
Accept pipeline input: True (ByValue)
Accept wildcard characters: False
```

### -ContentType
The HTTP content type

```yaml
Type: String
Parameter Sets: (All)
Aliases:

Required: False
Position: Named
Default value: application/json
Accept pipeline input: False
Accept wildcard characters: False
```

### -Method
The HTTP method to invoke.

```yaml
Type: String
Parameter Sets: (All)
Aliases:
Accepted values: GET, POST, PUT, DELETE

Required: False
Position: Named
Default value: GET
Accept pipeline input: False
Accept wildcard characters: False
```

### -Region
The AWS region the API is hosted in.

```yaml
Type: String
Parameter Sets: (All)
Aliases:

Required: True
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -SecretKey
The secret key of the IAM principal.

```yaml
Type: String
Parameter Sets: (All)
Aliases:

Required: True
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -Service
The AWS service you are invoking (e.g. "execute-api").

```yaml
Type: String
Parameter Sets: (All)
Aliases:

Required: False
Position: Named
Default value: execute-api
Accept pipeline input: False
Accept wildcard characters: False
```

### -Token
The session token when using temproary credentials.

```yaml
Type: String
Parameter Sets: (All)
Aliases:

Required: False
Position: Named
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### -Uri
The Uri to make the signed request to.

```yaml
Type: Uri
Parameter Sets: (All)
Aliases:

Required: True
Position: 0
Default value: None
Accept pipeline input: False
Accept wildcard characters: False
```

### CommonParameters
This cmdlet supports the common parameters: -Debug, -ErrorAction, -ErrorVariable, -InformationAction, -InformationVariable, -OutVariable, -OutBuffer, -PipelineVariable, -Verbose, -WarningAction, and -WarningVariable. For more information, see [about_CommonParameters](http://go.microsoft.com/fwlink/?LinkID=113216).

## INPUTS

### System.String

You can pipe the body of the request to this command.

## OUTPUTS

### AwsSigV4Cmdlet.BasicAwsSigV4WebResponse

## NOTES

## RELATED LINKS
