<%
' -----------------------------------------------------------------------------------------------------------
' (c) 2014 code to web. All rights reserved
'
' Kontakt  : support@code-to-web.com
' Anschrift: code to web, Flussgrabenstrasse 5A, 9445 Rebstein, Switzerland
' Telefon  : +41 / 71 / 534 3097
' WWW      : http://www.code-to-web.com
'
' Alle Rechte vorbehalten. Insbesondere sind Vervielfältigungen, Verbreitungen,
' Übersetzungen, die Be-, Um- und Verarbeitung und Einspeicherung 
' in elektronische Systeme urheberrechtlich geschützt. Eine Verwertung ist nur 
' mit Zustimmung von code to web zulässig.
' Rechtsverletzungen werden zivil- und strafrechtlich verfolgt. 
'
' Die Software, auch auszugsweise, darf nicht ohne vorherige Genehmigung von 
' code to web dritten gegenüber weitergegeben und zugünglich gemacht werden.
'
' Änderungen des Quelltextes sind ausschliesslich nur dann zulässig und
' im folgenden Rahmen gestattet wenn:
'
' 1. Copyrightvermerke und Kommentare unverändert bleiben.
' 2. Eigenschaften der Software nicht verändert, bzw. erweitert werden,
'    durch Bereitstellung zusätzlicher Funktionalitäten, die 
'    code to web in Ihrem Angebot hat und zugekauft werden können.
' 3. Einschränkungen von Funktionalitäten der Software entfernt oder geändert
'    werden, insbesondere bei der Prüfung von Registrationsschlüsseln.
' 4. Ausgaben von Copyrightinformationen müssen erhalten bleiben. Hierzu
'    zählen auch "print" Anweisungen mit Copyrightinformationen.
' 5. In den geänderten Modulen auf die Veränderung, deren Zeitpunkt 
'    und den Urheber der Veränderung hingewiesen wird.
' 6. Der geänderte Quelltext, ohne ausdrückliche Genehmigung von
'    code to web, dritten gegenüber nicht weitergeben und 
'    zugänglich gemacht wird. Abgeänderte Module dürfen nur auf dem erworbenen 
'    System angewandt werden.
' 7. Die geänderten Module dürfen ausschliesslich in diesem System
'    betrieben werden und nicht in anderen Systemen integriert und genutzt werden.
'    Ansonsten ist zuvor eine schriftliche Ermächtigung seitens code to web 
'    einzuholen.
' 8. code to web übernimmt keine Garantie und Gewährleistung
'    für die Systemsicherheit und die Funktionsfähigkeit des Systems bei
'    abgeänderten Modulen.
' 9. Änderungen müssen code to web mitgeteilt und zugänglich 
'    gemacht werden, um diese in das System zu integrieren, damit alle 
'    Kunden von Verbesserungen profitieren.
'
' -----------------------------------------------------------------------------------------------------------

Public Function HTMLEncode(strText)
  strText = Replace(strText, Chr(38), "&amp;" )
  strText = Replace(strText, Chr(34), "&quot;")
  strText = Replace(strText, Chr(60), "&lt;"  )
  strText = Replace(strText, Chr(62), "&gt;"  )
  HTMLEncode = strText
End Function

%>
<!DOCTYPE html>
<html>
<head>
  <title>Transfering session variables</title>
  <meta charset="utf-8"/>
  <meta http-equiv="X-UA-Compatible" content="IE=edge"/>
  <meta name="cache-control" http-equiv="cache-control" content="no-cache"/>
  <meta name="expires" http-equiv="expires" content="-1"/>
  <meta name="pragma" http-equiv="pragma" content="no-cache"/>
  <link rel="SHORTCUT ICON" href="code-to-web.ico" type="image/x-icon"/>
  <link href="stylesheets/screen.css" media="screen, projection" rel="stylesheet" type="text/css"/>
  <script type="text/javascript">
    x = screen.width;
    y = screen.height;
    w = x * 0.8;
    h = y * 0.8;
    self.resizeTo(w, h);
    self.moveTo((x - w) / 2, (y - h) / 2);
  </script>
</head>
<body>
  <div id="loading"><div id="ajaxloader"></div><div id="text">Please wait a moment</div></div>
  <form action="default.aspx" method="post">
    <%
      response.flush()
      For Each Item In Session.Contents
        response.flush()
        If NOT IsArray(Session(Item)) AND NOT IsObject(Session(Item)) Then
          response.write "<input type=""hidden"" name=""" & Item & """ value=""" & HTMLEncode(Session(Item)) & """/>"
        End If
      Next
    %>
  </form>
  <script type="text/javascript">
    document.getElementsByTagName("FORM")[0].submit();
  </script>
</body>
</html>