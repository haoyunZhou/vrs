﻿UPDATE [Flights]
   SET [SessionID]              = @sessionID
      ,[AircraftID]             = @aircraftID
      ,[StartTime]              = @startTime
      ,[EndTime]                = @endTime
      ,[Callsign]               = @callsign
      ,[NumPosMsgRec]           = @numPosMsgRec
      ,[NumADSBMsgRec]          = @numADSBMsgRec
      ,[NumModeSMsgRec]         = @numModeSMsgRec
      ,[NumIDMsgRec]            = @numIDMsgRec
      ,[NumSurPosMsgRec]        = @numSurPosMsgRec
      ,[NumAirPosMsgRec]        = @numAirPosMsgRec
      ,[NumAirVelMsgRec]        = @numAirVelMsgRec
      ,[NumSurAltMsgRec]        = @numSurAltMsgRec
      ,[NumSurIDMsgRec]         = @numSurIDMsgRec
      ,[NumAirToAirMsgRec]      = @numAirToAirMsgRec
      ,[NumAirCallRepMsgRec]    = @numAirCallRepMsgRec
      ,[FirstIsOnGround]        = @firstIsOnGround
      ,[LastIsOnGround]         = @lastIsOnGround
      ,[FirstLat]               = @firstLat
      ,[LastLat]                = @lastLat
      ,[FirstLon]               = @firstLon
      ,[LastLon]                = @lastLon
      ,[FirstGroundSpeed]       = @firstGroundSpeed
      ,[LastGroundSpeed]        = @lastGroundSpeed
      ,[FirstAltitude]          = @firstAltitude
      ,[LastAltitude]           = @lastAltitude
      ,[FirstVerticalRate]      = @firstVerticalRate
      ,[LastVerticalRate]       = @lastVerticalRate
      ,[FirstTrack]             = @firstTrack
      ,[LastTrack]              = @lastTrack
      ,[FirstSquawk]            = @firstSquawk
      ,[LastSquawk]             = @lastSquawk
      ,[HadAlert]               = @hadAlert
      ,[HadEmergency]           = @hadEmergency
      ,[HadSPI]                 = @hadSPI
      ,[UserNotes]              = @userNotes
 WHERE [FlightID] =               @flightID;
