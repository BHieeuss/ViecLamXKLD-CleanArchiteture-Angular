@echo off
setlocal enabledelayedexpansion

:: Đường dẫn tới thư mục hiện tại
set "folderPath=%cd%"

:: Phần cũ của tên thư mục cần thay đổi
set "oldPart=ChuyenNganh"

:: Phần mới của tên thư mục
set "newPart=ViecLam"

:: Ghi log vào tệp
set "logFile=%folderPath%\rename_log.txt"
echo Rename operations log > "%logFile%"
echo =================================== >> "%logFile%"

:: Duyệt qua tất cả các thư mục và tệp trong thư mục gốc và tất cả các thư mục con của nó
for /r "%folderPath%" %%i in (.) do (
    if "%%~nxi" neq "." (
        :: Lấy tên thư mục hiện tại
        set "currentFolder=%%~nxi"

        :: Kiểm tra nếu tên thư mục chứa oldPart
        echo Checking folder: !currentFolder!
        if not "%%~nxi" == "%oldPart%" (
            :: Tạo tên mới bằng cách thay thế oldPart bằng newPart
            set "newFolderName=!currentFolder:%oldPart%=%newPart%!"
            
            :: Đổi tên thư mục nếu có thay đổi
            if not "!newFolderName!" == "!currentFolder!" (
                ren "%%i" "!newFolderName!"
                echo Renamed folder "!currentFolder!" to "!newFolderName!"
                echo Renamed folder "!currentFolder!" to "!newFolderName!" >> "%logFile%"
            )
        )
    )
)

:: Duyệt qua tất cả các tệp trong thư mục gốc và tất cả các thư mục con của nó
for /r "%folderPath%" %%j in (*.*) do (
    :: Lấy tên tệp hiện tại
    set "currentFile=%%~nxj"

    :: Kiểm tra nếu tên tệp chứa oldPart
    echo Checking file: !currentFile!
    if not "%%~nxj" == "%oldPart%" (
        :: Tạo tên mới bằng cách thay thế oldPart bằng newPart
        set "newFileName=!currentFile:%oldPart%=%newPart%!"
        
        :: Đổi tên tệp nếu có thay đổi
        if not "!newFileName!" == "!currentFile!" (
            ren "%%j" "!newFileName!"
            echo Renamed file "!currentFile!" to "!newFileName!"
            echo Renamed file "!currentFile!" to "!newFileName!" >> "%logFile%"
        )
    )
)

echo Rename operations completed. See log file for details.
echo Rename operations completed. See log file for details. >> "%logFile%"
pause