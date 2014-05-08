Imports System.IO
Imports System.IO.Compression
Imports System.BitConverter
Module Module1

    Sub Main()
        Console.BufferHeight = 1024
        Dim BlocksMagic As Byte() = {7, 0, 6, 66, 108, 111, 99, 107, 115, 0, 0, 16, 0} 'Magic number for "Blocks" tag
        Dim DataMagic As Byte() = {7, 0, 4, 68, 97, 116, 97, 0, 0, 8, 0} 'Magic number for "Data" tag
        Dim TileEntitiesMagic As Byte() = {9, 0, 12, 84, 105, 108, 101, 69, 110, 116, 105, 116, 105, 101, 115, 0, 0, 0, 0, 0, 1, 0, 16}
        '   Magic number for "TileEntities" tag
        Dim BlockNames As String() = {"Air", "Stone", "Grass Block", "Dirt", "Cobblestone", "Wood Planks", "Sapling", "Bedrock", "Water (Flowing)", "Water (Source)", "Lava (Flowing)", "Lava (Source)", "Sand", "Gravel", "Gold Ore", "Iron Ore", "Coal Ore", "Wood", "Leaves", "Sponge", "Glass", "Lapis Lazuli Ore", "Lapis Lazuli Block", "Dispenser", "Sandstone", "Note Block", "Bed", "Powered Rail", "Detector Rail", "Sticky Piston", "Cobweb", "Tall Grass", "Dead Bush", "Piston", "Piston Extension", "Wool", "Piston-Moved Block", "Dandelion", "Poppy", "Brown Mushroom", "Red Mushroom", "Block of Gold", "Block of Iron", "Double Stone Slab", "Stone Slab", "Bricks (Block)", "TNT", "Bookshelf", "Moss Stone", "Obsidian", "Torch", "Fire", "Monster Spawner", "Wood Stairs", "Chest", "Redstone Wire", "Diamond Ore", "Block of Diamond", "Crafting Table", "Wheat", "Farmland", "Furnace", "Burning Furnace", "Sign", "Wooden Door", "Ladder", "Rail", "Cobblestone Stairs", "Sign (Wall)", "Lever", "Stone Pressure Plate", "Iron Door", "Wooden Pressure Plate", "Redstone Ore", "Redstone Ore (Glowing)", "Redstone Torch (Inactive)", "Redstone Torch (Active)", "Stone Button", "Snow (Cover)", "Ice", "Snow (Block)", "Cactus", "Clay (Block)", "Sugar Cane", "Jukebox", "Fence", "Pumpkin", "Netherrack", "Soul Sand", "Glowstone (Block)", "Nether Portal", "Jack o'Lantern", "Cake (Block)", "Redstone Repeater (Inactive)", "Redstone Repeater (Active)", "Stained Glass", "Trapdoor", "Monster Egg", "Stone Bricks", "Huge Brown Mushroom", "Huge Red Mushroom", "Iron Bars", "Glass Pane", "Melon", "Pumpkin Stem", "Melon Stem", "Vines", "Fence Gate", "Brick Stairs", "Stone Brick Stairs", "Mycelium", "Lily Pad", "Nether Brick", "Nether Brick Fence", "Nether Brick Stairs", "Nether Wart", "Enchantment Table", "Brewing Stand", "Cauldron", "End Portal", "End Portal Frame", "End Stone", "Dragon Egg", "Redstone Lamp (Inactive)", "Redstone Lamp (Active)", "Double Wooden Slab", "Wooden Slab", "Cocoa Pod", "Sandstone Stairs", "Emerald Ore", "Ender Chest", "Tripwire Hook", "Tripwire", "Block of Emerald", "Spruce Wood Stairs", "Birch Wood Stairs", "Jungle Wood Stairs", "Command Block", "Beacon", "Cobblestone Wall", "Flower Pot", "Carrots", "Potatoes", "Wooden Button", "Mob Head", "Anvil", "Trapped Chest", "Weighted Pressure Plate (Light)", "Weighted Pressure Plate (Heavy)", "Redstone Comparator", "[Unused]", "Daylight Sensor", "Block of Redstone", "Nether Quartz Ore", "Hopper", "Block of Quartz", "Quartz Stairs", "Activator Rail", "Dropper", "Stained Clay", "Stained Glass Pane", "Acacia Leaves", "Acacia Wood", "Acacia Wood Stairs", "Dark Oak Wood Stairs", "Slime Block", "Barrier", "Iron Trapdoor", "[Unused]", "[Unused]", "Hay Bale", "Carpet", "Hardened Clay", "Block of Coal", "Packed Ice", "Sunflower", "[Unused]", "[Unused]", "[Unused]", "[Unused]", "[Unused]", "[Unused]", "[Unused]", "[Unused]", "[Unused]", "[Unused]", "[Unused]", "[Unused]", "[Unused]", "[Unused]", "[Unused]", "[Unused]", "[Unused]", "[Unused]", "[Unused]", "[Unused]", "[Unused]", "[Unused]", "[Unused]", "[Unused]", "[Unused]", "[Unused]", "[Unused]", "[Unused]", "[Unused]", "[Unused]", "[Unused]", "[Unused]", "[Unused]", "[Unused]", "[Unused]", "[Unused]", "[Unused]", "[Unused]", "[Unused]", "[Unused]", "[Unused]", "[Unused]", "[Unused]", "[Unused]", "[Unused]", "[Unused]", "[Unused]", "[Unused]", "[Unused]", "[Unused]", "[Unused]", "[Unused]", "[Unused]", "[Unused]", "[Unused]", "[Unused]", "[Unused]", "[Unused]", "[Unused]", "[Unused]", "[Unused]", "[Unused]", "[Unused]", "[Unused]", "[Unused]", "[Unused]", "[Unused]", "[Unused]", "[Unused]", "[Unused]", "[Unused]", "[Unused]", "[Unused]", "[Unused]", "[Unused]", "[Unused]", "[Unused]", "[Unused]", "[Unused]", "[Unused]"}

        Console.WriteLine("========================")
        Console.WriteLine(" MINECRAFT DATA COUNTER")
        Console.WriteLine(" By Cat333Pokémon")
        Console.WriteLine("========================" & vbCrLf)
        'GET LIST OF WORLDS
        Dim folderlist As ObjectModel.ReadOnlyCollection(Of String)
        Try
            folderlist = My.Computer.FileSystem.GetDirectories(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) & "\.minecraft\saves")
        Catch ex As Exception
            Console.WriteLine("No Minecraft installation was found in the standard location.")
            Console.ReadKey()
            End
        End Try
        Dim i As Integer = 1
        For Each folder As String In folderlist
            If File.Exists(folder & "\level.dat") Then
                Console.WriteLine(i & ". " & My.Computer.FileSystem.GetDirectoryInfo(folder).Name)
                i += 1
            End If
        Next folder
        If i < 2 Then
            Console.WriteLine("No worlds were found.")
            Console.ReadKey()
            End
        End If
        Dim getone As String = ""
35:
        Console.Write(vbCrLf & "Select world: ")
        getone = Console.ReadLine()

        If Not IsNumeric(getone) Then
            Console.WriteLine("Invalid input. Try again.")
            getone = ""
            GoTo 35
        End If
        If Val(getone) > i - 1 Or Val(getone) < 1 Then
            Console.WriteLine("Invalid input. Try again.")
            getone = ""
            GoTo 35
        End If

        Dim foldernamelength As UInteger = My.Computer.FileSystem.GetDirectoryInfo(folderlist.Item(Val(getone - 1))).Name.Length
        Console.Write(vbCrLf & "===============")
        For l As UInteger = 1 To foldernamelength : Console.Write("=") : Next l
        Console.WriteLine(vbCrLf & " Loading """ & My.Computer.FileSystem.GetDirectoryInfo(folderlist.Item(Val(getone - 1))).Name & """...")
        Console.Write("===============")
        For l As UInteger = 1 To foldernamelength : Console.Write("=") : Next l
        Console.WriteLine(vbCrLf)

        Dim grandcomptotal As UInt64 = 0
        Dim granddecomptotal As UInt64 = 0
        Dim blockstats(255) As UInt64
        For q = 0 To 255
            blockstats(q) = 0
        Next
        Dim filetotal As UInteger = 0

        For Each location In New String() {"", "\DIM-1", "\DIM1"}


            'GET LIST OF ANVIL (.mca) REGION FILES
            Dim mcalist As ObjectModel.ReadOnlyCollection(Of String)
            Try
                mcalist = My.Computer.FileSystem.GetFiles(folderlist.Item(Val(getone - 1)) & location & "\region", FileIO.SearchOption.SearchTopLevelOnly, {"*.mca"})
            Catch ex As Exception
                Select Case location
                    Case ""
                        Console.WriteLine("This world does not have an Overworld. Ignored." & vbCrLf)
                    Case "\DIM-1"
                        Console.WriteLine("This world does not have a Nether. Ignored." & vbCrLf)
                    Case "\DIM1"
                        Console.WriteLine("This world does not have an End. Ignored." & vbCrLf)
                    Case Else
                        Console.WriteLine("Invalid location: " & location & ". This shouldn't happen.")
                End Select
                GoTo 56
                'Console.ReadKey()
                'End
            End Try
            Dim j As Integer = 1


            For Each mca As String In mcalist
                Console.WriteLine("Loading """ & My.Computer.FileSystem.GetDirectoryInfo(mca).Name & """...")


                'READ REGION FILE
                Dim fs As New FileStream(mca, FileMode.Open, FileAccess.Read)
                Dim br As New BinaryReader(fs)
                Dim data As Byte() = br.ReadBytes(My.Computer.FileSystem.GetFileInfo(mca).Length)
                Console.WriteLine(FormatNumber(Math.Ceiling((data.Length) / 1024), 0, , , TriState.True) & " KB loaded. Decompressing...")
                br.Close()
                fs.Close()

                Dim offsets(1023) As UInteger 'What nutcase designed VB.NET to make 0 = 1 when initializing arrays?
                Dim curbits(2) As Byte
                For r = 0 To 1023 'Get list of areas, exactly 1024 entries
                    curbits = data.Skip(r * 4).Take(3).Reverse.ToArray
                    offsets(r) = BitConverter.ToUInt32(New Byte(3) {curbits(0), curbits(1), curbits(2), 0}, 0) * 4096
                    'Console.WriteLine(offsets(r))
                Next r




                'Dim offset As UInteger = 8192
                Dim dtotal As UInt64 = 0
                Dim numread As UInteger = 0
                For r = 0 To 1023 'Here we go again :P
                    'DECOMPRESS
                    Dim complength As UInteger = BitConverter.ToUInt32(data.Skip(offsets(r)).Take(4).Reverse.ToArray, 0) 'I blame endianness
                    'Console.WriteLine(complength)
                    Dim comptype As Byte = data(offsets(r) + 4)
                    Dim dbuff As Byte()
                    If comptype = 1 Then
                        'IT IS CURRENTLY UNKNOWN IF GZIP WOULD WORK, AS ANVIL USES ZLIB DEFLATE. THIS MAY FAIL.
                        dbuff = data.Skip(offsets(r) + 5).Take(complength - 1).ToArray()
                    ElseIf comptype = 2 Then
                        dbuff = data.Skip(offsets(r) + 7).Take(complength - 3).ToArray() '2 for zlib header at front, -1 at end because of MC specs
                    End If
                    If comptype = 1 Or comptype = 2 Then
                        Dim io As New IO.MemoryStream(dbuff)
                        Dim dcomp As Object
                        Dim decompresseddata(9999999) As Byte
                        Dim decomplength As UInteger
                        If comptype = 1 Then
                            dcomp = New GZipStream(io, CompressionMode.Decompress, False)
                        ElseIf comptype = 2 Then
                            dcomp = New DeflateStream(io, CompressionMode.Decompress, False)
                        Else
                            Console.WriteLine("An invalid compression type is defined at " & Hex(offsets(r) + 4) & " (" & CInt(comptype) & "). Ignored.")
                        End If
                        Try
                            If comptype = 1 Or comptype = 2 Then
                                decomplength = dcomp.Read(decompresseddata, 0, 10000000)
                                'Console.WriteLine(decomplength)
                                'Console.WriteLine(decompresseddata.length)
                                'File.WriteAllBytes("E:\area" & offset & ".dat", decompresseddata.Take(decomplength).ToArray)
                                'Do Until gotten = "Blocks"

                                'Loop
                                'Find block sections (THIS IS NOT SAFE...BUT HIGHLY UNLIKELY TO BREAK UNLESS INTENTIONAL)
                                'Dim sections(4095)() As Byte
                                Dim foundmatch As Boolean = False
                                Dim f As Integer = 0, m As Integer = 0, p As Integer = 0
                                While f < decomplength
                                    foundmatch = False
                                    If decompresseddata(f) = CByte(7) Then
                                        For m = 1 To BlocksMagic.Length - 1
                                            If decompresseddata(f + m) <> BlocksMagic(m) Then
                                                Exit For
                                            End If
                                            If m = BlocksMagic.Length - 1 Then
                                                foundmatch = True
                                            End If
                                        Next m
                                        If foundmatch Then
                                            'Found the magic number, let's extract the blocks
                                            For m = 0 + BlocksMagic.Length To 4095 + BlocksMagic.Length
                                                'sections(m)(p) = decompresseddata(f + m)
                                                blockstats(decompresseddata(f + m)) += 1
                                                'Console.Write(CUInt(decompresseddata(f + m)) & " ")
                                                'If m Mod 16 = 0 Then Console.WriteLine()
                                                'If m Mod 256 = 0 Then Console.WriteLine()
                                            Next m
                                            p += 1
                                        End If
                                    End If
                                    f += 1
                                End While

                                dtotal += decomplength
                                numread += 1
                            End If
                        Catch
                            Console.WriteLine("Decompression failed in area at " & Hex(offsets(r)) & ". Ignored.")
                        End Try
                        'offset += (Math.Floor((complength + 5) / 4096) + 1) * 4096
                    End If
                Next r
                grandcomptotal += data.Length
                granddecomptotal += dtotal
                filetotal += 1
                Console.WriteLine(FormatNumber(numread, 0, , , TriState.True) & " sections decompressed to " & _
                                  FormatNumber(Math.Ceiling((dtotal) / 1024), 0, , , TriState.True) & " KB." & vbCrLf)

                j += 1
            Next mca
            If j < 2 Then
                mcalist = My.Computer.FileSystem.GetFiles(folderlist.Item(Val(getone - 1)) & location & "\region", FileIO.SearchOption.SearchTopLevelOnly, {"*.mcr"})
                Select Case location
                    Case ""
                        Console.Write("No Anvil (.mca) files were found in this world's Overworld. ")
                    Case "\DIM-1"
                        Console.Write("No Anvil (.mca) files were found in this world's Nether. ")
                    Case "\DIM1"
                        Console.Write("No Anvil (.mca) files were found in this world's End. ")
                    Case Else
                        Console.Write("Invalid location: " & location & ". This shouldn't happen. ")

                End Select

                If mcalist.Count > 0 Then
                    Console.Write(vbCrLf & "However, Region (.mcr) files were found. These will need to be converted to" & vbCrLf & _
                                      "Anvil by opening the world with Minecraft. ")
                Else
                    mcalist = My.Computer.FileSystem.GetFiles(folderlist.Item(Val(getone - 1)) & location & "\region", FileIO.SearchOption.SearchTopLevelOnly, {"*.dat"})
                    If mcalist.Count > 0 Then
                        Console.Write(vbCrLf & "However, Alpha (.dat) files were found. These will need to be converted to" & vbCrLf & _
                                      "Anvil by opening the world with Minecraft. ")
                    End If
                End If
                Console.WriteLine("Ignored." & vbCrLf)
                'Console.ReadKey()
                'End
            End If
56:
        Next location
        Console.WriteLine(FormatNumber(Math.Ceiling((grandcomptotal) / 1048576), 0, , , TriState.True) & " MB in " & _
                        FormatNumber(filetotal, 0, , , TriState.True) & " files decompressed to " & _
                        FormatNumber(Math.Ceiling((granddecomptotal) / 1048576), 0, , , TriState.True) & " MB.")

        Console.WriteLine(vbCrLf & "============" & vbCrLf & " Statistics" & vbCrLf & "============" & vbCrLf)

        For k = 1 To 175
            If blockstats(k) > 0 Then
                Console.Write(FormatNumber(blockstats(k), 0, , , TriState.True).PadLeft(15))
                Console.WriteLine("  " & BlockNames(k))
            End If
        Next

        Console.WriteLine(vbCrLf & "=======" & vbCrLf & " Done." & vbCrLf & "=======")
        Console.ReadKey()


    End Sub

End Module
