onException(){
    read -n 1 -p "Interrupted by error ..."; 
    exit 1;
}

read -p "Generated site location: " workspaceLocation

read -p "GitHub Repository: " remote

read -p "Local Repository Directory: " localDir
localDirPath=$localDir/Project.DocFX

{
  echo "=====> Cloning github pages branch from $remote to $localDirPath"
  git clone $remote -b gh-pages $localDirPath 
} || onException

{
  echo "=====> Moving to $localDirPath"
  cd $localDirPath
} || onException

{
  echo "=====> Removing old files from git"
  git rm -rf *

} || onException

{
  echo "=====> Copying generated files to current folder"
  cp -r $workspaceLocation/* .
} || onException

{
  echo "=====> Add, commit and push generated files to github"
  git add .
  git commit -m 'generated documents'
  git push
} || onException

{
  echo "=====> Clean up folders on $localDirPath"
  rm -rf $localDirPath
} || onException

read -n 1 -p "Deploy Done !!!"