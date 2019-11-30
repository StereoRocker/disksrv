# This file is part of disksrv.
# 
# Disksrv is free software: you can redistribute it and/or modify
# it under the terms of the GNU General Public License as published by
# the Free Software Foundation, either version 3 of the License, or
# (at your option) any later version.
# 
# Disksrv is distributed in the hope that it will be useful,
# but WITHOUT ANY WARRANTY; without even the implied warranty of
# MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
# GNU General Public License for more details.
# 
# You should have received a copy of the GNU General Public License
# along with disksrv.  If not, see <https://www.gnu.org/licenses/>.


#
#       Makefile for WATTCP applications
#

# Directories - change these!
TCC_INCLUDE=C:\prog\borlandc\include
WATTCP_INCLUDE=c:\clibs\include
WATTCP_LIB=c:\clibs\lib

# Compiler variables
DEBUG= D        # set to D for disable, E for enable
MODEL= S        # set to L for large, S for small

# Do not touch - this automatically sets up the inclusion of debug symbols
!if '$(DEBUG)'=='E'
IDEBUG=-v
TEXTDEBUG=enabled
!elif '$(DEBUG)'=='D'
IDEBUG=-v-
TEXTDEBUG=disabled
!else
!error  DEBUG must be set to either E or D
!endif

# Do not touch - this automatically sets up the library linking for WATTCP
!if '$(MODEL)'=='L'
CMODEL=-ml
CLIB=$(WATTCP_LIB)\wattcplg.lib
TEXTMODEL=large
!elif '$(MODEL)'=='S'
CMODEL=-ms
CLIB=$(WATTCP_LIB)\wattcpsm.lib
TEXTMODEL=small
!else
!error  MODEL must be set to either S or L
!endif

# Sets C compiler flags + command line
CFLAGS= $(CMODEL) -r- $(IDEBUG) -I$(TCC_INCLUDE) -I$(WATTCP_INCLUDE)
CC= bcc $(CFLAGS)

# Executable targets

.c.exe:
        $(CC) $*.c $(CLIB)


disksrv.exe: disksrv.c
        $(CC)  disksrv.c $(CLIB)

