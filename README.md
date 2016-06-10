# Eindopdracht security 2 - Encryptie

Ik heb voor de eindopdracht een Asp.NET C# webapplicatie gemaakt, met een MySQL database

Ik maak gebruik van Aes encryptie om de tekst te encrypten en decrypten.
De database slaat alleen de naam van de persoon die het bericht verstuurd op en de encrypted tekst. Alleen met het correct wachtwoord kan de tekst gelezen worden.
Er is een dropbox om de teksten die in de database staan te selecteren.

Als er geen Naam/Tekst/Wachtwoord is ingevuld, zal er niets worden opgeslagen.
Als er geen Wachtwoord is ingevuld, zal de text niet worden decrypt.
