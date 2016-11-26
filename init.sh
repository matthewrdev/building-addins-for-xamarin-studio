echo "INIT: Cleaning old workspace..."
./clean.sh

echo "INIT: Building docs bundle..."
xbuild MFractor/Products/MFractor.Documentation/MFractor.Documentation.mdproj

echo "INIT: Opening solution..."
nuget restore ./MFractor/MFractor.sln
