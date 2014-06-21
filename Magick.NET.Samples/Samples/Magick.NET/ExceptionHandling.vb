﻿'==================================================================================================
'  Copyright 2013-2014 Dirk Lemstra <https://magick.codeplex.com/>
' 
'  Licensed under the ImageMagick License (the "License"); you may not use this file except in 
'  compliance with the License. You may obtain a copy of the License at
' 
'    http://www.imagemagick.org/script/license.php
' 
'  Unless required by applicable law or agreed to in writing, software distributed under the
'  License is distributed on an "AS IS" BASIS, WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either
'  express or implied. See the License for the specific language governing permissions and
'  limitations under the License.
'==================================================================================================

Imports System
Imports ImageMagick

Namespace RootNamespace.Samples.MagickNET

	Public NotInheritable Class ExceptionHandlingSamples

		Private Shared Sub MagickImage_Warning(sender As Object, arguments As WarningEventArgs)
			Console.WriteLine(arguments.Message)
		End Sub

		Public Shared Sub ExceptionHandling()
			Try
				' Read invalid jpg file
				Using image As New MagickImage(SampleFiles.InvalidFileJpg)
				End Using
				' Catch any MagickException
			Catch exception As MagickException
				' Write excepion raised when reading the invalid jpg to the console
				Console.WriteLine(exception.Message)
			End Try

			Try
				' Read corrupt jpg file
				Using image As New MagickImage(SampleFiles.CorruptImageJpg)
				End Using
				' Catch only MagickCorruptImageErrorException
			Catch exception As MagickCorruptImageErrorException
				' Write excepion raised when reading the corrupt jpg to the console
				Console.WriteLine(exception.Message)
			End Try
		End Sub

		Public Shared Sub ObtainWarningThatOccurredDuringRead()
			' Read file that will raise a warning.
			Using image As New MagickImage(SampleFiles.FileWithWarningJpg)
				' Check if warning was set and write to console
				If image.ReadWarning IsNot Nothing Then
					Console.WriteLine(image.ReadWarning.Message)
				End If
			End Using

			Using image As New MagickImage()
				' Read file that will raise a warning.
				Dim warning As MagickWarningException = image.Read(SampleFiles.FileWithWarningJpg)
				' Check if warning was returned and write to console
				If warning IsNot Nothing Then
					Console.WriteLine(warning.Message)
				End If
			End Using

			Using image As New MagickImage()
				' Attach event handler to warning event
				AddHandler image.Warning, AddressOf MagickImage_Warning
				' Read file that will raise a warning.
				image.Read(SampleFiles.FileWithWarningJpg)
			End Using
		End Sub

	End Class

End Namespace